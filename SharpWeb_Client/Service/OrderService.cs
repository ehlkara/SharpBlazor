using Newtonsoft.Json;
using Sharp_Models;
using SharpWeb_Client.Service.IService;
using System.Text;

namespace SharpWeb_Client.Service
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<OrderDto> Create(StripePaymentDto paymentDto)
        {
            var content = JsonConvert.SerializeObject(paymentDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/orders/create", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderDto>(responseResult);
                return result;
            }
            return new OrderDto();
        }

        public async Task<OrderDto> Get(int orderHeaderId)
        {
            var response = await _httpClient.GetAsync($"/api/orders/{orderHeaderId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderDto>(content);
                return order;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDto>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAll(string? userId = null)
        {
            var response = await _httpClient.GetAsync("/api/orders");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(content);

                return orders;
            }

            return new List<OrderDto>();
        }
    }
}
