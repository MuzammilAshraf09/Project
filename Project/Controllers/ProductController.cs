using Microsoft.AspNetCore.Mvc;
using Project.Models.Entities;
using Project.Models.Interfaces;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductReviewRepository _productReviewRepository;

        public ProductController(IProductRepository productRepository, IProductReviewRepository productReviewRepository)
        {
            _productRepository = productRepository;
            _productReviewRepository = productReviewRepository;
        }

        // GET: Product/Index (Admin View)
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        // GET: Product/Index2 (User View)
        public IActionResult Index2()
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

            var reviews = _productReviewRepository.GetByProductId( id);
            ViewBag.Reviews = reviews;

            ViewData["Title"] = $"Details {product.Name}";
            return View(product);
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

        // GET: Product/Update/5
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["Title"] = $"Edit {product.Name}";
            return View(product);
        }

        // POST: Product/Update/5
        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        // GET: Product/Review/5
        public IActionResult Review(int id)
        {
            // Retrieve UserId from cookies
            var userIdCookie = Request.Cookies["UserId"];
            int userId = string.IsNullOrEmpty(userIdCookie) ? 0 : int.Parse(userIdCookie);

            if (userId == 0)
            {
                return RedirectToAction("Login", "User"); // Redirect to login if UserId is missing
            }

            // Get the product by its ID
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            // Initialize a new review model with ProductId and UserId
            var model = new ProductReview
            {
                ProductId = id,
                UserId = userId
            };

            // Pass ProductId via ViewBag (if needed in the view)
            ViewBag.ProductId = id;

            return View(model);
        }

        // POST: Product/Review/5
        [HttpPost]
        public IActionResult Review(ProductReview review)
        {
            // Validate the model
            if (ModelState.IsValid)
            {
                // Set the review date to the current date and time
                review.ReviewDate = DateTime.Now;

                // Add the review to the repository
                _productReviewRepository.Add(review);

                // Redirect to the product details page after successful review submission
                return RedirectToAction("Details", new { id = review.ProductId });
            }

            // If model validation fails, return the view with the model data
            return View(review);
        }



    }
}
