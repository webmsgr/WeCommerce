using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WeCommerce.Data;
using WeCommerce.Models;

namespace WeCommerce.Controllers
{
    public class UsersController : Controller
    {
        private readonly WeCommerceContext _context;


        public UsersController(WeCommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        public async Task<IActionResult> Login(UserLogin login)
        {

            if (!ModelState.IsValid)
            {
                return View(login); // nuh uh
            }
            // find the user by username
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username);
            if (user == null)
            {
                ModelState.AddModelError("Username", "Invalid Username/Password");
                ModelState.AddModelError("Password", "Invalid Username/Password");
                return View(login);
            }
            // thank you me for the Matches method.
            if (login.Matches(user))
            {
                // log em in
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Username", "Invalid Username/Password");
                ModelState.AddModelError("Password", "Invalid Username/Password");
                return View(login);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegister user)
        {
            if (ModelState.IsValid)
            {
                // convert to regular user
                var actualUser = user.ToUser();
                // Save user to database

                _context.Users.Add(actualUser);
                try
                {
                    await _context.SaveChangesAsync(); // this will throw on error, not a good idea.
                }
                // thanks stack overflow https://stackoverflow.com/questions/3967140/duplicate-key-exception-from-entity-framework
                catch (DbUpdateException ex)
                    when (ex.InnerException is SqlException sqlException &&
                          (sqlException.Number == 2627 || sqlException.Number == 2601))
                {
                    // idk man
                    ModelState.AddModelError("Username", "Username/Email already exists");
                    ModelState.AddModelError("Email", "Username/Email already exists");
                    return View(user);
                }

                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}
