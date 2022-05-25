using System.ComponentModel.DataAnnotations;

namespace Sharp_DataAccess.ViewModel
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
