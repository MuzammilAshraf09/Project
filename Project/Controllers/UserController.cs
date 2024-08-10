using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var users = _userRepository.GetAll();
            User user = null;

            foreach (var u in users)
            {
                if (u.Username == username && u.Password == password)
                {
                    user = u;
                    break;
                }
            }

            if (user != null)
            {
                // Set cookie
                Response.Cookies.Append("username", username, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    Secure = true
                });
                return RedirectToAction("Index", "User");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                var users = _userRepository.GetAll();
                bool userExists = false;

                foreach (var u in users)
                {
                    if (u.Username == user.Username)
                    {
                        userExists = true;
                        break;
                    }
                }

                if (!userExists)
                {
                    _userRepository.Add(user);
                    // Automatically log in after signing up
                    Response.Cookies.Append("username", user.Username, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        Secure = true
                    });
                    return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError("", "Username already exists.");
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("username");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            // Check if user is logged in
            var isLoggedIn = Request.Cookies["username"] != null;
            ViewData["IsLoggedIn"] = isLoggedIn;
            ViewData["Username"] = isLoggedIn ? Request.Cookies["username"] : string.Empty;
            return View();
        }
    }
}
