using Application.Abstractions.Repositories;
using food_delivery.Models;
using food_delivery.ViewModels;
using System.Web.Mvc;

namespace food_delivery.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository<Category> _categoryRepository;

        public CategoriesController(ICategoryRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var allCategories = await _categoryRepository.GetAll();

            List<CategoryViewModel> categoryViewModel = allCategories
                .ToList()
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList();

            return View(categoryViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            CategoryViewModel category = new CategoryViewModel();

            return View(category);
        }
        [HttpPost]
        public async Task<RedirectToRouteResult> Create(CategoryViewModel vm)
        {
            Category model = new Category()
            {
                Title = vm.Title,
            };

            await _categoryRepository.Create(model);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            var category = await _categoryRepository.GetById(id);
            var viewModel = new CategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<RedirectToRouteResult> Edit(CategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetById(vm.Id);
                if (category != null)
                {
                    category.Title = vm.Title;
                    _categoryRepository.Update(vm.Id, category);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<RedirectToRouteResult> Delete(int id)
        {

            await _categoryRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}