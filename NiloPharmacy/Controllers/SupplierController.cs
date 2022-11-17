using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xyzpharmacy.Data;
using Microsoft.AspNetCore.Authorization;
using xyzpharmacy.Data.Services;
using xyzpharmacy.Models;
using xyzpharmacy.Data.Static;

namespace xyzpharmacy.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class SupplierController : Controller
    {

        private readonly ISupplierService _service;

        public SupplierController(ISupplierService Service)
        {
            _service = Service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierName","SupplierAddress")]Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(supplier);
                
            }
            await _service.AddAsync(supplier);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int Id)
        {
            var SupplierDetails =  await _service.GetByIdAsync(Id);
            if (SupplierDetails == null)
            {
                return View("NotFound");
            }
            return View(SupplierDetails);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            var supplier = await _service.GetByIdAsync(Id);
            if(supplier == null)
            {
                return View("NotFound");
            }
            return View(supplier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id,[Bind("SupplierId","SupplierName","SupplierAddress")] Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(supplier);

            }
            await _service.UpdateAsync(Id,supplier);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int Id)
        {
            var supplier = await _service.GetByIdAsync(Id);
            if (supplier == null)
            {
                return View("NotFound");
            }
            return View(supplier);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var supplier = await _service.GetByIdAsync(Id);
            if (supplier == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(Id);
            
            return RedirectToAction(nameof(Index));

        }
    }
}
