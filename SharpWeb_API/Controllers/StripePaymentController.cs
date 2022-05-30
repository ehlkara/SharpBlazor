using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sharp_Models;
using Stripe.Checkout;

namespace SharpWeb_API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StripePaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDto paymentDto)
        {
            try
            {
                var domain = _configuration.GetValue<string>("Sharp_Client_URL");

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + paymentDto.SuccessUrl,
                    CancelUrl = domain + paymentDto.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                foreach (var item in paymentDto.Order.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);
                return Ok(new SuccessModelDto()
                {
                    Data = session.Id
                });
            }
            catch (Exception ex)
            {

                return BadRequest(new ErrorModelDto()
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
