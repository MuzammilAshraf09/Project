﻿using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;
using Project.Models.Repositories;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController()
        {
            _productRepository = new ProductRepository(); // Hardcoded repository instance
        }

        // GET: Product
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        // GET: Product/Details/5
        public IActionResult Details(int id)
        {
            var product = _productRepository.GetById(id);
           
            return View();
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            

            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            
            return View();
        }

        // POST: Product/Delete/
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index2()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }
        public IActionResult Review(int id)
        {
            var product = _productRepository.GetById(id);
            return View();

        }
    }
}
