using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using xyzpharmacy.Data;
using Microsoft.AspNetCore.Authorization;
using xyzpharmacy.Data.Services;
using xyzpharmacy.Models;
using xyzpharmacy.Data.Static;
using System.Security.Cryptography;
using XYZPharmacy.Data.ViewModels;

namespace xyzpharmacy.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        

        
        private readonly IProductsService _service;

        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductsController(IProductsService Service, IWebHostEnvironment hostEnvironment)
        {
            _service = Service;
            webHostEnvironment = hostEnvironment;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewData["Welcome"] = "Welcome to our XYZ Pharmacy";
            ViewBag.Description = "XYZ Pharmacy is your go-to online pharmacy store " +
                "for all your medicine needs – be it your regular medications, or over-the-counter (OTC) medicines.";
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> CreateAsync()
        {
            
            var movieDropdownsData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Suppliers = new SelectList(movieDropdownsData.Suppliers, "SupplierId", "SupplierName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = "~/Image/" + UploadedFile(model);

                Product prod = new Product()
                {
                    ProductName = model.ProductName,
                    ProductPrice = model.ProductPrice,
                    CategoryName = model.CategoryName,
                    SupplierId = model.SupplierId,
                    Stock = model.Stock,
                    ExpiryDate = model.ExpiryDate,
                    MedicinalUse = model.MedicinalUse,
                    MedicineDesc = model.MedicineDesc,
                    ProductImage = uniqueFileName,
                };

                await _service.AddAsync(prod);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int Id)
        {
            var SupplierDetails = await _service.GetByIdAsync(Id);
            if (SupplierDetails == null)
            {
                return View("Not Found");
            }
            return View(SupplierDetails);
        }


        public async Task<IActionResult> Edit(int Id)
        {
            var movieDropdownsData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Suppliers = new SelectList(movieDropdownsData.Suppliers, "SupplierId", "SupplierName");
            var model = await _service.GetByIdAsync(Id);
            if (model == null)
            {
                return View("Not Found");
            }
            ProductsViewModel supplier = new ProductsViewModel()
            {
                ProductName = model.ProductName,
                ProductPrice = model.ProductPrice,
                CategoryName = model.CategoryName,
                SupplierId = model.SupplierId,
                Stock = model.Stock,
                ExpiryDate = model.ExpiryDate,
                MedicinalUse = model.MedicinalUse,
                MedicineDesc = model.MedicineDesc,
            };

            return View(supplier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductsViewModel model)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = "~/Image/" + UploadedFile(model);

                Product prod = new Product()
                {
                    ProductName = model.ProductName,
                    ProductPrice = model.ProductPrice,
                    CategoryName = model.CategoryName,
                    SupplierId = model.SupplierId,
                    Stock = model.Stock,
                    ExpiryDate = model.ExpiryDate,
                    MedicinalUse = model.MedicinalUse,
                    MedicineDesc = model.MedicineDesc,
                    ProductImage = uniqueFileName,
                };

                await _service.UpdateAsync(id, prod);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProd = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                
                var filteredResult = allProd.Where(n => n.ProductName.Contains(searchString) || n.MedicineDesc.Contains(searchString) || n.MedicinalUse.ToString().Contains(searchString) || n.CategoryName.ToString().Contains(searchString)).ToList();
                //var filteredResultNew = allProd.Where(n => string.Equals(n.ProductName, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.MedicineDesc, searchString,
                //    StringComparison.CurrentCultureIgnoreCase)||(string.Equals(n.CategoryName.ToString(), searchString, StringComparison.CurrentCultureIgnoreCase))).ToList();
                return View("Index", filteredResult);
                
            }

            return View("Index", allProd);
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
        [HttpPost, ActionName("Delete")]
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

        private string UploadedFile(ProductsViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Image");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
