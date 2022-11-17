using xyzpharmacy.Models;

namespace xyzpharmacy.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);

        Task<List<Order>> GetOrders();

        Task DeleteAsync(int Id);
    }
}
