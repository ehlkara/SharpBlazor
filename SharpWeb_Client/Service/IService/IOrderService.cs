using Sharp_Models;

namespace SharpWeb_Client.Service.IService
{
    public interface IOrderService
    {
        Task<OrderDto> Get(int orderId);
        Task<IEnumerable<OrderDto>> GetAll(string? userId);
        Task<OrderDto> Create(StripePaymentDto paymentDto);
    }
}
