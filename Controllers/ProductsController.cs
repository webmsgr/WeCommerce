using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
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
    }
}
