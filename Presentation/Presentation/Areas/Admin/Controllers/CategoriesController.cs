using Application.Abstractions.Services;
using Application.Models;
using AutoMapper;
using Presentation.ViewModels;
using System.Web.Mvc;

namespace food_delivery.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var allCategories = await categoryService.GetAll();

            List<CategoryViewModel> categoryViewModel =
                mapper.ProjectTo<CategoryViewModel>(allCategories).ToList();

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
            var mappedCategory = mapper.Map<CategoryModel>(vm);

            await categoryService.Create(mappedCategory);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ViewResult> Edit(int id)
        {
            var category = await categoryService.GetById(id);

            var viewModel = mapper.Map<CategoryViewModel>(category);

            return View(viewModel);
        }
        [HttpPost]
        public async Task<RedirectToRouteResult> Edit(CategoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var category = await categoryService.GetById(vm.Id);

                await categoryService.Update(category.Id, category);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<RedirectToRouteResult> Delete(int id)
        {

            await categoryService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}