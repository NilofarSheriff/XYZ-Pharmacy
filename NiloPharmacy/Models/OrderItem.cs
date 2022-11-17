using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace xyzpharmacy.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public decimal Amount { get; set; }
        public decimal Price { get; set; }

        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order order { get; set; }
    }
}
