using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using System.Collections.Generic;

namespace Project.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ICartItemsRepository _cartItemsRepository;

        public CartItemsController()
        {
            _cartItemsRepository = new CartItemsRepository();
        }

        // GET: CartItem
        public IActionResult Index(int userId)
        {
            var cartItems = _cartItemsRepository.GetCartItemsByUserId(userId);
            return View(cartItems);
        }

        // GET: CartItem/Details/5
        public IActionResult Details(int id)
        {
            var cartItem = _cartItemsRepository.GetCartItemById(id);
           
            return View();
        }

        // GET: CartItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartItem/Create
        [HttpPost]
        public IActionResult Create(CartItems cartItem)
        {
            if (ModelState.IsValid)
            {
                _cartItemsRepository.AddCartItem(cartItem);
                return RedirectToAction(nameof(Index), new { userId = cartItem.UserId });
            }
            return View(cartItem);
        }

        // GET: CartItem/Delete/5
        public IActionResult Delete(int id)
        {
            var cartItem = _cartItemsRepository.GetCartItemById(id);
           
            return View();
        }

        // POST: CartItem/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cartItem = _cartItemsRepository.GetCartItemById(id);
            if (cartItem != null)
            {
                _cartItemsRepository.RemoveCartItem(id);
            }
            return RedirectToAction(nameof(Index), new { userId = cartItem?.UserId });
        }
    }
}
