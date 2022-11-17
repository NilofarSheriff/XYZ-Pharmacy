using Microsoft.AspNetCore.Mvc;
using xyzpharmacy.Models;
using xyzpharmacy.Data.Services;
using xyzpharmacy.Data.Static;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _service;

        public ProductController(IProductsService Service)
        {
            _service = Service;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var data = await _service.GetAllAsync();
            return data;
            
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var SupplierDetails = await _service.GetByIdAsync(id);
            if(SupplierDetails == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(SupplierDetails);
            }
            
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product prod)
        {
            await _service.AddAsync(prod);
            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Product prod)
        {
            await _service.UpdateAsync(id,prod);
            return Ok();

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();

        }

        [HttpGet("{searchString}")]
        public async Task<IEnumerable<Product>> Filter([FromRoute]string searchString)
        {
            var allProd = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {

                var filteredResult = allProd.Where(n => n.ProductName.Contains(searchString) || n.MedicineDesc.Contains(searchString) || n.MedicinalUse.ToString().Contains(searchString) || n.CategoryName.ToString().Contains(searchString)).ToList();
                //var filteredResultNew = allProd.Where(n => string.Equals(n.ProductName, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.MedicineDesc, searchString,
                //    StringComparison.CurrentCultureIgnoreCase)||(string.Equals(n.CategoryName.ToString(), searchString, StringComparison.CurrentCultureIgnoreCase))).ToList();
                return filteredResult;

            }
            return allProd;


        }
    }
}
