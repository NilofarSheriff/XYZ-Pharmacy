using Microsoft.AspNetCore.Mvc;
using xyzpharmacy.Models;
using xyzpharmacy.Data.Services;
using xyzpharmacy.Data.Static;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService Service)
        {
            _service = Service;
        }
        // GET: api/<SupplierController>
        [HttpGet]
        public async Task<IEnumerable<Supplier>> Get()
        {
            var data = await _service.GetAllAsync();
            return data;
        }

        // GET api/<SupplierController>/5
        [HttpGet("{id}")]
        public async Task<Supplier> Get(int id)
        {
            var supplier = await _service.GetByIdAsync(id);
            
            return supplier;
        }

        // POST api/<SupplierController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Supplier supplier)
        {
            await _service.AddAsync(supplier);
            return Ok();
        }

        // PUT api/<SupplierController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Supplier supplier)
        {
            await _service.UpdateAsync(id, supplier);
            return Ok();
        }

        // DELETE api/<SupplierController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
