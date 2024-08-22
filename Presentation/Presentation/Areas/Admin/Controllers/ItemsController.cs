using Application.Abstractions.Repositories;
using food_delivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;

namespace food_delivery.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ItemsController : Controller
    {
        private readonly ItemService _itemRepository;

        public ItemsController(ItemService itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _itemRepository.GetPopulated();

            var items = _context.Items
                .Include(x => x.Category)
                .Include(y => y.Subcategory)
                .Select(model => new ItemViewModel()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                    SubcategoryId = model.SubcategoryId,
                })
                .ToList();
            return View(items);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ItemViewModel vm = new ItemViewModel();
            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Subcategory = new SelectList(_context.Subcategories, "Id", "Title");
            return View(vm);
        }
        [HttpGet]
        public IActionResult GetSubcategory(ItemApiModel item)
        {
            ItemViewModel vm = new ItemViewModel()
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.CategoryId,
            };
            //var subcategory = _context.Subcategories.Where(x => x.CategoryId == id).ToJson();
            ViewBag.Subcategory = new SelectList(_context.Subcategories.Where(x => x.CategoryId == item.CategoryId), "Id", "Title");
            //return Json(subcategory);
            return View("Create", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel vm)
        {
            ItemApiModel model = new ItemApiModel();
            if (ModelState.IsValid)
            {
                if (vm.ImageUrl != null && vm.ImageUrl.Length > 0)
                {
                    var uploadDir = @"Images/Items";
                    var filename = Guid.NewGuid().ToString() + "-" + vm.ImageUrl.FileName;
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, uploadDir, filename);
                    await vm.ImageUrl.CopyToAsync(new FileStream(path, FileMode.Create));
                    model.Image = "/" + uploadDir + "/" + filename;
                    
                }
                model.Id = vm.Id;
                model.Title = vm.Title;
                model.Price = vm.Price;
                model.Description = vm.Description;
                model.CategoryId = vm.CategoryId;
                model.SubcategoryId = vm.SubcategoryId;
                
                _context.Items.Add(model);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit (int id)
        {
            var item = _context.Items.Where(x => x.Id == id).FirstOrDefault();
            ItemViewModel vm = new ItemViewModel();
            vm.Id = id;
            vm.Title = item.Title;
            vm.Description = item.Description;
            vm.Price = item.Price;
            vm.CategoryId = item.CategoryId;
            vm.SubcategoryId = item.SubcategoryId;

            ViewBag.Category = new SelectList(_context.Categories, "Id", "Title");
            ViewBag.Subcategory = new SelectList(_context.Subcategories, "Id", "Title");

            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(ItemViewModel vm)
        {
            ItemApiModel model = _context.Items.Where(x => x.Id == vm.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                model.Id = vm.Id;
                model.Title = vm.Title;
                model.Description = vm.Description;
                model.Price = vm.Price;
                model.CategoryId = vm.CategoryId;
                model.SubcategoryId = vm.SubcategoryId;

                _context.Items.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vm);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var item = _context.Items.Where(x => x.Id == id).FirstOrDefault();
            
            _context.Remove(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
