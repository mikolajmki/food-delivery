using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.ApiModels;
using Presentation.ViewModels;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SubcategoriesController : Controller
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public SubcategoriesController(
            ISubcategoryService subcategoryService, 
            IMapper mapper, 
            ICategoryService categoryService
        )
        {
            _subcategoryService = subcategoryService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var list = await _subcategoryService.GetAllIncludeCategory();
            var subcategories = _mapper.Map<List<SubcategoryApiModel>>(list);

            return View(subcategories);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var list = await _categoryService.GetAll();
            var categoryVMs = _mapper.Map<List<CategoryViewModel>>(list);

            ViewBag.Category = new SelectList(categoryVMs, "Id", "Title");

            var subcategory = new SubcategoryViewModel();

            return View(subcategory);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubcategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var subcategory = _mapper.Map<SubcategoryModel>(vm);

                await _subcategoryService.Create(subcategory);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var subcategory = await _subcategoryService.GetFirstSubcategoryOfCategoryId(id);
            var categories = await _categoryService.GetAll();

            var vm = new SubcategoryViewModel
            {
                Id = subcategory.Id,
                Title = subcategory.Title,
            };

            ViewBag.category = new SelectList(categories, "Id", "Title", subcategory.CategoryId);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SubcategoryViewModel vm)
        {
            var subcategory = _mapper.Map<SubcategoryModel>(vm);

            await _subcategoryService.Update(subcategory.Id, subcategory);

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _subcategoryService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
