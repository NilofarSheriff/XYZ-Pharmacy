using xyzpharmacy.Models;

namespace xyzpharmacy.Data.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier> GetByIdAsync(int Id);

        Task AddAsync(Supplier supplier);
        Task<Supplier> UpdateAsync(int Id,Supplier supplier);
        Task DeleteAsync(int Id);

    }
}
