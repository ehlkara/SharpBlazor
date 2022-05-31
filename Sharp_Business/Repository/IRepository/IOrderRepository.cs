using Sharp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Business.Repository.IRepository
{
    public interface IOrderRepository
    {
        Task<OrderDto> Get(int id);
        Task<IEnumerable<OrderDto>> GetAll(string? userId = null, string? status = null);
        Task<OrderDto> Create(OrderDto objDto);
        Task<int> Delete(int id);

        Task<OrderHeaderDto> UpdateHeader(OrderHeaderDto objDto);
        Task<OrderHeaderDto> MarkPaymentSuccessful(int id);
        Task<bool> UpdateOrderStatus(int orderId, string status);
        Task<OrderHeaderDto> CancelOrder(int id);
    }
}
