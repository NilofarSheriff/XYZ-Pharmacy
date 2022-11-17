using Microsoft.EntityFrameworkCore;
using xyzpharmacy.Models;

namespace xyzpharmacy.Data.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;
        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var result = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == Id);
            _context.Suppliers.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            var result = await _context.Suppliers.ToListAsync();
            return result;
        }

        public async Task<Supplier> GetByIdAsync(int Id)
        {
            var result = await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierId == Id);
            return result;
        }

        public async Task<Supplier> UpdateAsync(int Id, Supplier supplier)
        {
            _context.Update(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

    }
}