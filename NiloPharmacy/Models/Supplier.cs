using System.ComponentModel.DataAnnotations;

namespace xyzpharmacy.Models
{
    public class Supplier
    {
        [Key]
        [Display(Name = "Supplier Id")]
        public int SupplierId { get; set; }
        [Display(Name = "Supplier Name")]
        [Required(ErrorMessage ="Supplier Name is Required")]
        [StringLength(100,MinimumLength =3, ErrorMessage ="Name should have minimum 3 Letters")]
        
        public string SupplierName { get; set; }
        [Display(Name = "Supplier Address")]
        [Required(ErrorMessage = "Supplier  Address is Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should have minimum 3 Letters")]
        public string SupplierAddress { get; set; }
        //RelationShip
        //public List<Product> Products { get; set; }
        
    }
}
