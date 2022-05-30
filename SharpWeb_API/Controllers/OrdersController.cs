using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sharp_Business.Repository.IRepository;
using Sharp_Models;
using Stripe.Checkout;

namespace SharpWeb_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderRepository.GetAll());
        }

        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            if(orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorModelDto()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }

            var orderHeader = await _orderRepository.Get(orderHeaderId.Value);
            if(orderHeader == null)
            {
                return BadRequest(new ErrorModelDto()
                {
                    ErrorMessage = "Order not found.",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(orderHeader);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDto paymentDto)
        {
            paymentDto.Order.OrderHeader.OrderDate = DateTime.Now;
            var result = await _orderRepository.Create(paymentDto.Order);
            return Ok(result);
        }
        [HttpPost]
        [ActionName("paymentsuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDto orderHeaderDto)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(orderHeaderDto.SessionId);
            if(sessionDetails.PaymentStatus == "paid")
            {
                var result = await _orderRepository.MarkPaymentSuccessful(orderHeaderDto.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorModelDto() { ErrorMessage = "Can not mark payment as successful" });
                }
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
