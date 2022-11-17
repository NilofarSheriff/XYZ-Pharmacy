using System.ComponentModel.DataAnnotations;
using xyzpharmacy.Data.Enums;
using xyzpharmacy.Models;

namespace XYZPharmacy.Data.ViewModels
{
    public class ProductsViewModel
    {
        [Key]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should have minimum 3 Letters")]
        public string ProductName { get; set; }
        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category Name is Required")]
        public Category CategoryName { get; set; }
        [Display(Name = "Stock Quantity")]
        [Required(ErrorMessage = "No. of stock Required")]
        public int Stock { get; set; }
        [Display(Name = "Medicinal use")]
        [Required(ErrorMessage = "Medicinal Use is Required")]
        public MedicinalUse MedicinalUse { get; set; }
        [Required(ErrorMessage = "Expiry Date is Required")]
        [ExpiryDate(ErrorMessage = "Expiry Date must be greater than Today's Date")]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }
        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product Price is Required")]
        public Decimal ProductPrice { get; set; }
        [Display(Name = "Medicine Description")]
        [Required(ErrorMessage = "Medicine Description is Required")]
        public string MedicineDesc { get; set; }

        [Display(Name = "Product Image")]
        [Required(ErrorMessage = "Product Image is Required")]
        public IFormFile ProductImage { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Supplier Id")]
        [Required(ErrorMessage = "Supplier Id is Required")]
        public int SupplierId { get; set; }
    }
}
