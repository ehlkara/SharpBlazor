using Sharp_Models;

namespace SharpWeb_Client.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> Get(int productId);
    }
}
