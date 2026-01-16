using Atelier2.Models;
using Atelier2.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Atelier2.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryRepository CategRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            CategRepository = categoryRepository;
        }

        [AllowAnonymous]
        // ✅ GET: CategoryController
        public ActionResult Index()
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var allCategories = _categoryRepository.GetAll();
            return View(allCategories);
        }

        // ✅ GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var category = _categoryRepository.GetById(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        // ✅ GET: CategoryController/Create
        public ActionResult Create()
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // ✅ GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var category = _categoryRepository.GetById(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // ✅ GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var categories = CategRepository.GetAll();
            ViewData["Categories"] = categories;

            var category = _categoryRepository.GetById(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
