namespace Project.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Project.Models.Entities;
    using Project.Models.Interfaces;
    using Project.Models.Repositories;



    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        // Constructor injection of ICategoryRepository
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: Category
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAllCategories();
            return View(categories);
        }

        // GET: Category/Details/5
        public IActionResult Details(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
           
            return View();
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public IActionResult Edit( Category category)
        {
            

            if (ModelState.IsValid)
            {
                _categoryRepository.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
           
            return View();
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
