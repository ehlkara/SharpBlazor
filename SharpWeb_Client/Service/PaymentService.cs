using Newtonsoft.Json;
using Sharp_Models;
using SharpWeb_Client.Service.IService;
using System.Text;

namespace SharpWeb_Client.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SuccessModelDto> Checkout(StripePaymentDto model)
        {
            try
            {
                var content = JsonConvert.SerializeObject(model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);
                string responseResult = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SuccessModelDto>(responseResult);
                    return result;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModelDto>(responseResult);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
