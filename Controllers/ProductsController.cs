using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeCommerce.Data;
using WeCommerce.Models;

namespace WeCommerce.Controllers
{
    public class ProductsController : Controller {

        private readonly WeCommerceContext _context;

        public ProductsController(WeCommerceContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            int page = id ?? 1;
            const int PageSize = 10;
            if (page < 1)
            {
                return NotFound();
            }
            var products = await _context.Products
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            var productPageCount = (int)Math.Ceiling(await _context.Products.CountAsync() / (double)PageSize);
            
            var model = new ProductCatalogPageViewModel
            {
                Products = products,
                PageCount = productPageCount,
                CurrentPage = page
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!((bool?)HttpContext.Items["isAdmin"] ?? false))
            {
                return Unauthorized(); // nah
            }
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }




        public async Task<IActionResult> Edit(Product product)
        {
            if (!((bool?)HttpContext.Items["isAdmin"] ?? false))
            {
                return Unauthorized(); // nah
            }
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();


                TempData["message"] = "Product updated successfully";

                return RedirectToAction("Index");
            }
            return View(product);
        }


        [HttpGet]
        public IActionResult Create()
        {
            if (!((bool?)HttpContext.Items["isAdmin"] ?? false))
            {
                return Unauthorized(); // nah
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!((bool?)HttpContext.Items["isAdmin"] ?? false))
            {
                return Unauthorized(); // nah
            }
            if (ModelState.IsValid)
            {
                // add to DB
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                // show success message
                ViewData["message"] = "Product added successfully";
                return View();
            }

            return View(product);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!((bool?)HttpContext.Items["isAdmin"] ?? false))
            {
                return Unauthorized(); // nah
            }
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!((bool?)HttpContext.Items["isAdmin"] ?? false))
            {
                return Unauthorized(); // nah
            }
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["message"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }




        public async Task<IActionResult> Details(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
