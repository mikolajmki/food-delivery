using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using MapsterMapper;
using Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ItemsController : Controller
    {
        private readonly IItemService itemService;
        private readonly ICategoryService categoryService;
        private readonly ISubcategoryService subcategoryService;
        private readonly IFileService fileService;
        private readonly IMapper mapper;

        public ItemsController(
            IItemService itemService, 
            ICategoryService categoryService, 
            ISubcategoryService subcategoryService,
            IMapper mapper
        )
        {
            this.itemService = itemService;
            this.categoryService = categoryService;
            this.subcategoryService = subcategoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var items = await itemService.GetPopulated();

            return View(items);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var categories = await categoryService.GetAll();
            var subcategories = await subcategoryService.GetAll();
            ViewBag.Category = new SelectList(categories, "Id", "Title");
            ViewBag.Subcategory = new SelectList(subcategories, "Id", "Title");

            return View(new ItemViewModel());
        }
        [HttpGet]
        public async Task<IActionResult> GetSubcategoryAsync(ItemViewModel item)
        {
            var subcategories = await subcategoryService.GetSubcategoriesOfCategoryId(item.CategoryId); 
            ViewBag.Subcategory = new SelectList(subcategories, "Id", "Title");
            //return Json(subcategory);
            return View("Create", item);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel vm)
        {
            if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
            {
                using(var fileStream = vm.ImageUrl.OpenReadStream())
                {
                    if (await fileService.SaveImage(fileStream))
                    {
                        var item = mapper.Map<ItemModel>(vm);
                        await itemService.Create(item);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            var item = await itemService.GetById(id);

            var vm = mapper.Map<ItemViewModel>(item);

            var categories = await categoryService.GetAll();
            var subcategories = await subcategoryService.GetAll();

            var categoryVMs = mapper.Map<List<CategoryViewModel>>(categories);
            var subcategoryVMs = mapper.Map<List<SubcategoryViewModel>>(subcategories);

            ViewBag.Category = new SelectList(categoryVMs, "Id", "Title");
            ViewBag.Subcategory = new SelectList(subcategoryVMs, "Id", "Title");

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(ItemViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var item = mapper.Map<ItemModel>(vm);

                if (await itemService.Update(vm.Id, item))
                {
                    return RedirectToAction("Index");
                }
            }

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await itemService.Delete(id))
            {
                return View("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
