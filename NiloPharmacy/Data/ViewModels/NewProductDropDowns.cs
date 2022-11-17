using xyzpharmacy.Models;

namespace xyzpharmacy.Data.ViewModels
{
    public class NewProductDropDownsVM
    {
        public NewProductDropDownsVM()
        {
            Suppliers = new List<Supplier>();
            
        }

        public List<Supplier> Suppliers { get; set; }
        
    }

}
