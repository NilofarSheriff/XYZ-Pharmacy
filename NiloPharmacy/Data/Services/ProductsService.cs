using Microsoft.EntityFrameworkCore;
using xyzpharmacy.Data.ViewModels;
using xyzpharmacy.Models;

namespace xyzpharmacy.Data.Services
{
    public class ProductsService:IProductsService
    {
        private readonly AppDbContext _context;
        public ProductsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product supplier)
        {
            await _context.Products.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == Id);
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var result = await _context.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetByIdAsync(int Id)
        {
           
            var result = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == Id);
            return result;
        }

        public async Task<Product> UpdateAsync(int Id, Product supplier)
        {
           Product Found =  _context.Products.ToList().Find(X=>X.ProductId==Id)!;
            if (Found != null)
            {
                Found.ProductName = supplier.ProductName;
                Found.ProductPrice = supplier.ProductPrice;
                Found.ProductImage = supplier.ProductImage;
                Found.CategoryName = supplier.CategoryName;
                Found.MedicinalUse = supplier.MedicinalUse;
                Found.SupplierId = supplier.SupplierId;
                Found.ExpiryDate = supplier.ExpiryDate;
                Found.MedicineDesc = supplier.MedicineDesc;
                Found.Stock = supplier.Stock;
            }
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task<NewProductDropDownsVM> GetNewProductsDropdownsValues()
        {
            var response = new NewProductDropDownsVM()
            {
                Suppliers = await _context.Suppliers.OrderBy(n => n.SupplierId).ToListAsync(),
                
            };

            return response;
        }

        
    }
}
