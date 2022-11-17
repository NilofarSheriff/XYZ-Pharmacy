using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using xyzpharmacy.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using xyzpharmacy.Data.Services;

namespace xyzpharmacy.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        private readonly IProductsService _service;

       


        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
            
        }
    

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddItemToCart(Product prod)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.product.ProductId == prod.ProductId && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null && prod.Stock>0)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    product = prod,
                    Amount = 1
                };

                

                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else if(prod.Stock>0)
            {
                shoppingCartItem.Amount++;
                

            }
            else
            {
                
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Product prod)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.product.ProductId == prod.ProductId && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                   

                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                   
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.product).ToList());
        }
        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public decimal GetShoppingCartTotal() => _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.product.ProductPrice * n.Amount).Sum();


    }

}
