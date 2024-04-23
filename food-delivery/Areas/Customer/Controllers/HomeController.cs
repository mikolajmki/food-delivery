using food_delivery.Areas.Admin.Controllers;
using food_delivery.Models;
using food_delivery.Repository;
using food_delivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace food_delivery.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            ItemListViewModel vm = new ItemListViewModel()
            {
                CategoriesList = await _context.Categories.ToListAsync(),
                Categories = await _context.Categories.ToListAsync(),
                Coupons = await _context.Coupons.Where(c => c.IsActive).ToListAsync(),
            };

            vm.Items = _context.Items.Include(x => x.Category).Include(y => y.Subcategory)
            .Select(model =>
                new ItemViewModel()
                {
                    Id = model.Id,
                    Image = model.Image,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                    TotalReviewsCount = _context.Reviews.Where(x => x.ItemId == model.Id).Count(),
                }).ToList();

            foreach (var item in vm.Items)
            {
                if (item.TotalReviewsCount > 0)
                {
                    item.AverageRating = (double)_context.Reviews.Where(x => x.ItemId == item.Id).ToList().Sum(x => x.Rating) / (double)item.TotalReviewsCount;
                }
                else
                {
                    item.AverageRating = 0;
                }
            }

            return View(vm);
        }

        public IActionResult GetByCategory (int id)
        {
            ItemListViewModel vm = new ItemListViewModel()
            {
                CategoriesList = _context.Categories.ToList(),
                Categories = _context.Categories.ToList(),
                Coupons = _context.Coupons.Where(c => c.IsActive).ToList(),
            };

            vm.Items = _context.Items.Where(x => x.CategoryId == id).Include(x => x.Category).Include(y => y.Subcategory)
            .Select(model =>
                new ItemViewModel()
                {
                    Id = model.Id,
                    Image = model.Image,
                    Title = model.Title,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                    TotalReviewsCount = _context.Reviews.Where(x => x.ItemId == model.Id).Count(),
                }).ToList();

            foreach (var item in vm.Items)
            {
                if (item.TotalReviewsCount > 0)
                {
                    item.AverageRating = (double)_context.Reviews.Where(x => x.ItemId == item.Id).ToList().Sum(x => x.Rating) / (double)item.TotalReviewsCount;
                }
                else
                {
                    item.AverageRating = 0;
                }
            }

            return View("Index", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var itemFromDb = await _context.Items.Include(x => x.Category).Include(y => y.Subcategory).Where(x => x.Id == id).FirstOrDefaultAsync();
            var reviews = _context.Reviews.Include(x => x.ApplicationUser).Where(x => x.ItemId == id).ToList();

            var vm = new ItemDetailsViewModel()
            {
                Item = itemFromDb,
                ItemId = itemFromDb.Id,
                Reviews = reviews,
                Count = 1
            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(ItemDetailsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                vm.ApplicationUserId = claim.Value;

                Console.WriteLine(vm.ToString());

                var cartFromDb = await _context.Carts.Where(x => x.ApplicationUserId == vm.ApplicationUserId && x.ItemId == vm.ItemId).FirstOrDefaultAsync();

                if (cartFromDb == null) 
                {
                    await _context.Carts.AddAsync(new Cart
                    {
                        ItemId = vm.ItemId,
                        ApplicationUserId = vm.ApplicationUserId,
                        Count = vm.Count
                    });
                } else
                {
                    cartFromDb.Count += vm.Count;
                }

                await _context.SaveChangesAsync();
                
                var userCartCount = _context.Carts.Where(x => x.ApplicationUserId == claim.Value).ToList().Count();
                HttpContext.Session.SetInt32("SessionCart", userCartCount);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
