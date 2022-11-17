using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using xyzpharmacy.Data;
using xyzpharmacy.Data.Enums;
using xyzpharmacy.Data.Services;
using xyzpharmacy.Models;
using System.Runtime.CompilerServices;

namespace Pharmacy.Tests
{
   

    [TestFixture]

    public class Tests
    {

        private readonly AppDbContext context;
        
        public Tests()
        {
            DbContextOptionsBuilder dboptions = new DbContextOptionsBuilder().UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new AppDbContext(dboptions.Options);
        }

        [Test]
       public async Task GetProducts()
        {
            var products = new ProductsService(context);
            var list = await products.GetAllAsync();
            
            Assert.Pass();
        }

        [Test]
        public async Task AddProducts()
        {
            var products = new ProductsService(context);
            
            Product prod = new Product()
            {
                ProductName = "Citrizine",
                ProductPrice = 40,
                CategoryName = Category.Tablet,
                MedicinalUse = MedicinalUse.Cold,
                ProductImage = "~/Image/3.jpeg",
                SupplierId = 2,
                ExpiryDate = new DateTime(2027, 11, 29),
                MedicineDesc = "Citrizine is an antihistamine used to relieve allergy symptoms " +
            "such as watery eyes, runny nose, itching eyes/nose, sneezing, hives, and itching. " +
            "It works by blocking a certain natural substance (histamine) that your " +
            "body makes during an allergic reaction."
            };
            await products.AddAsync(prod);
            Assert.Pass();
        }


        [Test]
        public async Task EditProducts()
        {
            var products = new ProductsService(context);
            Product prod = new Product()
            {
                ProductName = "Dolo650",
                ProductPrice = 40,
                CategoryName = Category.Tablet,
                MedicinalUse = MedicinalUse.Fever,
                ProductImage = "~/Image/1.jpeg",
                SupplierId = 2,
                ExpiryDate = new DateTime(2027, 11, 29),
                MedicineDesc = "Dolo 650 Tablet helps relieve pain and fever by blocking the" +
                           " release of certain chemical messengers responsible for fever and pain."
            };

            await products.UpdateAsync(1, prod);
            Assert.Pass();
            }

        [Test]
        public async Task DeleteProducts()
        {
            var products = new ProductsService(context);
            await products.DeleteAsync(1);
            Assert.Pass();
        }
        [Test]
        public async Task AddSupplier()
        {
            var suppliers = new SupplierService(context);
            Supplier supplier1 = new Supplier()
            {
                SupplierName = "MMNZ ltd",
                SupplierAddress = "Salem, TamilNadu"

            };
            await suppliers.AddAsync(supplier1);
            Assert.Pass();
        }
        [Test]
        public async Task EditSupplier()
        {
            var suppliers = new SupplierService(context);

            Supplier supplier1 = new Supplier()
            {
                SupplierName = "MMNZ ltd",
                SupplierAddress = "TamilNadu,India"

            };
            await suppliers.UpdateAsync(1, supplier1);
            Assert.Pass();
        }
        [Test]
        public async Task DeleteSupplier()
        {
            var suppliers = new SupplierService(context);

            await suppliers.DeleteAsync(1);
            Assert.Pass();
        }
        [Test]
        public async Task GetSuppliers()
        {
            var suppliers = new SupplierService(context);

            await suppliers.GetAllAsync();
            Assert.Pass();

        }

        [Test]
        public async Task GetOrders()
        {
            var orders = new OrdersService(context);
            await orders.GetOrders();
            Assert.Pass();

        }
        [Test]
        public async Task GetOrderbyRole()
        {
            var orders = new OrdersService(context);
            await orders.GetOrdersByUserIdAndRoleAsync("","Admin");
            Assert.Pass();

        }
        [Test]
        public async Task StoreOrder()
        {
            var orders = new OrdersService(context);
            List<ShoppingCartItem> items = new List<ShoppingCartItem>()
            {
                new ShoppingCartItem()
                {
                    Id = 1,
                    ShoppingCartId="werr",
                    Amount=1,
                    UnitPrice=30,
                    product =new Product()
                    {
                        ProductId=1,
                        ProductName="dolo",
                        ProductPrice=30
                        
                    }

                    
                }
            };
            await orders.StoreOrderAsync(items, "1234", "mehar@gmail.com");
            Assert.Pass();
        }



    }

    
}