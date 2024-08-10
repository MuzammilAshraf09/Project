using Microsoft.AspNetCore.Mvc;
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
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public IActionResult Create([Bind("Name, Description, Price, ImageUrl, CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id, Name, Description, Price, ImageUrl, CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
