using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using System.Collections.Generic;
using System.Security.Claims;

namespace Project.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ICartItemsRepository _cartItemsRepository;
        private readonly IProductRepository _productRepository;
        public CartItemsController(ICartItemsRepository cartItemsRepository, IProductRepository productRepository)
        {
            _cartItemsRepository = cartItemsRepository;
            _productRepository = productRepository;
        }

        // GET: CartItem
        public IActionResult Index(int userId)
        {

            var cartItems = _cartItemsRepository.GetCartItemsByUserId(1);
            int count = 0;
            foreach (var item in cartItems)
            {
                count += 1;
                item.Product = _productRepository.GetById(item.ProductId); // Fetch the product for each cart item
            }
            Console.WriteLine(userId);
            Console.WriteLine(count);
            return View(cartItems);
        }

        // GET: CartItem/Details/5
        public IActionResult Details(int id)
        {
            var cartItem = _cartItemsRepository.GetById(id);
           
            return View();
        }

        // GET: CartItem/Create
        public IActionResult Create()
        {
            // Get the user ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Pass the user ID to the view using ViewBag or ViewData
            ViewBag.UserId = userId;

            return View();
        }
        // POST: CartItem/Create
        [HttpPost]
        public IActionResult Create(CartItems cartItem)
        {
            Console.WriteLine("recieved");
            if (ModelState.IsValid)
            {
               Console.WriteLine(cartItem.Id);
                _cartItemsRepository.Add(cartItem);
                return RedirectToAction(nameof(Index), new { userId = cartItem.UserId });
            }
            //foreach (var entry in ModelState)
            //{
            //    // Check if there are any errors in this entry
            //    if (entry.Value.Errors.Count > 0)
            //    {
            //        Console.WriteLine($"Key: {entry.Key}");

            //        // Log all errors for this entry
            //        foreach (var error in entry.Value.Errors)
            //        {
            //            Console.WriteLine($"Error: {error.ErrorMessage}");
            //            if (error.Exception != null)
            //            {
            //                Console.WriteLine($"Exception: {error.Exception.Message}");
            //            }
            //        }
            //    }
            //}
            //Console.WriteLine("getooted");
            return View(cartItem);
        }

        // GET: CartItem/Delete/5
        public IActionResult Delete(int id)
        {
            var cartItem = _cartItemsRepository.GetById(id);
           
            return View(cartItem);
        }

        // POST: CartItem/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cartItem = _cartItemsRepository.GetById(id);
            if (cartItem != null)
            {
                _cartItemsRepository.RemoveCartItem(id);
            }
            return RedirectToAction(nameof(Index), new { userId = cartItem?.UserId });
        }
    }
}
