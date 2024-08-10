namespace Project.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Project.Models.Entities;
    using Project.Models.Interfaces;
    using Project.Models.Repositories;



    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController()
        {
            _categoryRepository =  new CategoryRepository();
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
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public IActionResult Create([Bind("CategoryId, Name, Description")] Category category)
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
        public IActionResult Edit(int id, [Bind("CategoryId, Name, Description")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

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
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
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
