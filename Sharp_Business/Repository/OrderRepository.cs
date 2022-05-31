using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sharp_Business.Repository.IRepository;
using Sharp_Common;
using Sharp_DataAccess;
using Sharp_DataAccess.Data;
using Sharp_DataAccess.ViewModel;
using Sharp_Models;

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

        public async Task<OrderHeaderDto> CancelOrder(int id)
        {
            var orderHeader = await _context.OrderHeaders.FindAsync(id);
            if(orderHeader == null)
            {
                return new OrderHeaderDto();
            }

            if(orderHeader.Status == SD.Status_Pending)
            {
                orderHeader.Status = SD.Status_Cancelled;
                await _context.SaveChangesAsync();
            }
            if(orderHeader.Status == SD.Status_Confirmed)
            {
                //refund
                var options = new Stripe.RefundCreateOptions
                {
                    Reason = Stripe.RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId
                };

                var service = new Stripe.RefundService();
                Stripe.Refund refund = service.Create(options);

                orderHeader.Status = SD.Status_Refunded;
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<OrderHeader, OrderHeaderDto>(orderHeader);
        }

        public async Task<OrderDto> Create(OrderDto objDto)
        {
            try
            {
                var obj = _mapper.Map<OrderDto, Order>(objDto);
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
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDto>(obj.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDto>>(obj.OrderDetails).ToList()
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objDto;
        }

        public async Task<int> Delete(int id)
        {
            var objHeader = await _context.OrderHeaders.FirstOrDefaultAsync(f => f.Id == id);
            if (objHeader != null)
            {
                IEnumerable<OrderDetail> objDetail = _context.OrderDetails.Where(o => o.OrderHeaderId == id);

                _context.OrderDetails.RemoveRange(objDetail);
                _context.OrderHeaders.Remove(objHeader);
                return _context.SaveChanges();
            }
            return 0;
        }

        public async Task<OrderDto> Get(int id)
        {
            Order order = new()
            {
                OrderHeader = _context.OrderHeaders.FirstOrDefault(o => o.Id == id),
                OrderDetails = _context.OrderDetails.Where(o => o.OrderHeaderId == id),
            };
            if (order != null)
            {
                return _mapper.Map<Order, OrderDto>(order);
            }
            return new OrderDto();
        }

        public async Task<IEnumerable<OrderDto>> GetAll(string? userId = null, string? status = null)
        {
            List<Order> OrderFromDb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _context.OrderHeaders;
            IEnumerable<OrderDetail> orderDetailList = _context.OrderDetails;

            foreach (OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(o => o.OrderHeaderId == header.Id),
                };
                OrderFromDb.Add(order);
            }
            //do some filtering #TODO

            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(OrderFromDb);
        }

        public async Task<OrderHeaderDto> MarkPaymentSuccessful(int id)
        {
            var data = await _context.OrderHeaders.FindAsync(id);
            if (data == null)
            {
                return new OrderHeaderDto();
            }
            if (data.Status == SD.Status_Pending)
            {
                data.Status = SD.Status_Confirmed;
                await _context.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDto>(data);
            }
            return new OrderHeaderDto();
        }

        public async Task<OrderHeaderDto> UpdateHeader(OrderHeaderDto objDto)
        {
            if (objDto != null)
            {
                var orderHeaderFromDb = _context.OrderHeaders.FirstOrDefault(o => o.Id == objDto.Id);
                orderHeaderFromDb.Name = objDto.Name;
                orderHeaderFromDb.PhoneNumber = objDto.PhoneNumber;
                orderHeaderFromDb.Carrier = objDto.Carrier;
                orderHeaderFromDb.Tracking = objDto.Tracking;
                orderHeaderFromDb.StreetAddress = objDto.StreetAddress;
                orderHeaderFromDb.City = objDto.City;
                orderHeaderFromDb.State = objDto.State;
                orderHeaderFromDb.PostalCode = objDto.PostalCode;
                orderHeaderFromDb.Status = objDto.Status;

                await _context.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDto>(orderHeaderFromDb);
            }
            return new OrderHeaderDto();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _context.OrderHeaders.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }
            data.Status = status;
            if (status == SD.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
