using System.ComponentModel.DataAnnotations;

namespace xyzpharmacy.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Product product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }



        public string ShoppingCartId { get; set; }
    }
}
