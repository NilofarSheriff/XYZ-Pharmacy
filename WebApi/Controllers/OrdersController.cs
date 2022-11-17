using Microsoft.AspNetCore.Mvc;
using xyzpharmacy.Models;
using xyzpharmacy.Data.Services;
using xyzpharmacy.Data.Static;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService Service)
        {
            _service = Service;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<IEnumerable<Order>> Get()
        {
            var orders = await _service.GetOrders();
            return orders ;
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<Order>> Get(string id,string name)
        {
            var orders = await _service.GetOrdersByUserIdAndRoleAsync(id, name);
            return orders;
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<IActionResult> Post(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            await _service.StoreOrderAsync(items, userId, userEmailAddress);
            return Ok();

        }

        

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
