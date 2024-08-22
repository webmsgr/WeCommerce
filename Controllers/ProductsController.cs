using Microsoft.AspNetCore.Mvc;

namespace WeCommerce.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
