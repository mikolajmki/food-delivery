using food_delivery.Models;
using food_delivery.Repository;
using food_delivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace food_delivery.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubcategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubcategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var subcategory = _context.Subcategories
                .Include(x => x.Category)
                .ToList();

            return View(subcategory);
        }
        [HttpGet]
        public IActionResult Create()
        {
            SubcategoryViewModel vm = new SubcategoryViewModel();

            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title");

            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(SubcategoryViewModel vm)
        {
            SubcategoryApiModel model = new SubcategoryApiModel();
            if (ModelState.IsValid)
            {
                model.Title = vm.Title;
                model.CategoryId = vm.CategoryId;

                _context.Subcategories.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            SubcategoryViewModel vm = new SubcategoryViewModel();
            var subcategory = _context.Subcategories
                .Where(x => x.CategoryId == id)
                .FirstOrDefault();
            if (subcategory != null)
            {
                vm.Id = subcategory.Id;
                vm.Title = subcategory.Title;
                ViewBag.category = new SelectList(_context.Categories, "Id", "Title", subcategory.CategoryId);
            }

            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(SubcategoryViewModel vm)
        {
            SubcategoryApiModel model = _context.Subcategories.Where(x => x.Id == vm.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                model.Title = vm.Title;
                model.CategoryId = vm.CategoryId;

                _context.Subcategories.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var subcategory = _context.Subcategories
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (subcategory != null)
            {
                _context.Subcategories.Remove(subcategory);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
