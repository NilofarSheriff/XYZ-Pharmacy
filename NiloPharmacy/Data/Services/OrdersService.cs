using Microsoft.EntityFrameworkCore;
using xyzpharmacy.Models;

namespace xyzpharmacy.Data.Services
{
    public class OrdersService:IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int Id)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(x => x.Id == Id);
            _context.Orders.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            var result = await _context.Orders.ToListAsync();
            return result;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.product).Include(n => n.User).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.product.ProductId,
                    OrderId = order.Id,
                    Price = item.product.ProductPrice
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

        
    }
}
