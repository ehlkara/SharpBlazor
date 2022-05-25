using AutoMapper;
using Sharp_Business.Repository.IRepository;
using Sharp_DataAccess;
using Sharp_DataAccess.Data;
using Sharp_DataAccess.ViewModel;
using Sharp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> Create(OrderDto objDto)
        {
            try
            {
                var obj = _mapper.Map<OrderDto,Order>(objDto);
                _context.OrderHeaders.Add(obj.OrderHeader);
                await _context.SaveChangesAsync();

                foreach (var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;
                }
                _context.OrderDetails.AddRange(obj.OrderDetails);
                await _context.SaveChangesAsync();

                return new OrderDto()
                {
                    Id = 0,
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDto>(obj.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDto>>(obj.OrderDetails).ToList(),
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return objDto;
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDto>> GetAll(string? userId = null, string? status = null)
        {
            throw new NotImplementedException();
        }

        public Task<OrderHeaderDto> MarkPaymentSuccessful(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderHeaderDto> UpdateHeader(OrderHeaderDto objDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
