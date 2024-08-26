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
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        { 
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }




        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(product);
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
