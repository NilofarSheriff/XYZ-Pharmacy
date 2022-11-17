using xyzpharmacy.Data.ViewModels;
using xyzpharmacy.Models;

namespace xyzpharmacy.Data.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int Id);

        Task AddAsync(Product supplier);
        Task<Product> UpdateAsync(int Id, Product supplier);
        Task DeleteAsync(int Id);

        

        Task<NewProductDropDownsVM> GetNewProductsDropdownsValues();
    }
}
