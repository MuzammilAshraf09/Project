using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using System;

namespace ProjectWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;

        public AdminController()
        {
            _adminRepository = new AdminRepository();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var users = _adminRepository.GetAll();
            Admin user = null;

            foreach (var u in users)
            {
                if (u.Username == username && u.Password == password)
                {
                    user = u;
                    break;
                }
            }

            if (user == null)
            {
                Console.WriteLine("empty");
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
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
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
