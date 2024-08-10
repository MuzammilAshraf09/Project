using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;
using System.Collections.Generic;

namespace Project.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemRepository _cartItemRepository;

        public CartItemController()
        {
            _cartItemRepository = new CartItemRepository();
        }

        // GET: CartItem
        public IActionResult Index(int userId)
        {
            var cartItems = _cartItemRepository.GetCartItemsByUserId(userId);
            return View(cartItems);
        }

        // GET: CartItem/Details/5
        public IActionResult Details(int id)
        {
            var cartItem = _cartItemRepository.GetCartItemById(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }

        // GET: CartItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CartItem/Create
        [HttpPost]
        public IActionResult Create([Bind("ProductId, Quantity, UserId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _cartItemRepository.AddCartItem(cartItem);
                return RedirectToAction(nameof(Index), new { userId = cartItem.UserId });
            }
            return View(cartItem);
        }

        // GET: CartItem/Delete/5
        public IActionResult Delete(int id)
        {
            var cartItem = _cartItemRepository.GetCartItemById(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            return View(cartItem);
        }

        // POST: CartItem/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cartItem = _cartItemRepository.GetCartItemById(id);
            if (cartItem != null)
            {
                _cartItemRepository.RemoveCartItem(id);
            }
            return RedirectToAction(nameof(Index), new { userId = cartItem?.UserId });
        }
    }
}
