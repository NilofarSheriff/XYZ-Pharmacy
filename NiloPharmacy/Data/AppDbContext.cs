using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using xyzpharmacy.Models;

namespace xyzpharmacy.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //public AppDbContext(DbContextOptions options) : base(options)
        //{
        //}

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
