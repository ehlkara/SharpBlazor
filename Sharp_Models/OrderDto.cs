using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public OrderHeaderDto OrderHeader { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
