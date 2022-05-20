using Newtonsoft.Json;
using Sharp_Models;
using SharpWeb_Client.Service.IService;
using SharpWeb_Server.Pages;

namespace SharpWeb_Client.Service
{
    public class ProductService : IProductService
    {
        private HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto> Get(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDto>(content);
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDto>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(content);
                return products;
            }
            return new List<ProductDto>();
        }
    }
}
