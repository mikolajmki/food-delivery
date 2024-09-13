using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using MapsterMapper;
using Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allCategories = await categoryService.GetAll();

            var categoryViewModel =
                mapper.Map<List<CategoryViewModel>>(allCategories);

            return View(categoryViewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            CategoryViewModel category = new();

            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel vm)
        {
            var mappedCategory = mapper.Map<CategoryModel>(vm);

            await categoryService.Create(mappedCategory);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryService.GetById(id);

            var viewModel = mapper.Map<CategoryViewModel>(category);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryService.GetById(vm.Id);

                await categoryService.Update(category.Id, category);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            await categoryService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}