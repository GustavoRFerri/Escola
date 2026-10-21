using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Web.Models;
using Store.Web.Services.Contracts;

namespace Store.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            var result = await _productService.GetAllProducts();

            if (result == null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
            //var result = await _productService.FindProductById(id);
            //if (result == null)
            //{
            //    return View("Error");
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductViewModel productVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // The issue is here
                    var result = await _productService.CreateProduct(productVM);
                    if (result != null)
                        return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
                }
                return View(productVM);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
            var result = await _productService.FindProductById(id);
            if (result == null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(ProductViewModel productVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _productService.UpdateProduct(productVM);
                    if (result != null)
                        return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
                }
                return View(productVM);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.FindProductById(id);
            if (result == null)
            {
                return View("Error");
            }
            return View(result);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            try
            {
                var result = await _productService.DeleteProductById(id);
                if (result)
                    return RedirectToAction(nameof(Index));
                return View("Error");
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
