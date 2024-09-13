using Application.Abstractions.Services;
using Application.Models.Commands;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.ApiModels;
using Presentation.Areas.Identity;
using Presentation.ViewModels;

namespace Presentation.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        private readonly IReviewService _reviewService;
        private readonly ICartService _cartService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IItemService itemService, IMapper mapper, IReviewService reviewService, ICartService cartService, ILogger<HomeController> logger)
        {
            _itemService = itemService;
            _mapper = mapper;
            _reviewService = reviewService;
            _cartService = cartService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {

            var  itemListReadModel = await _itemService.GetItemList();
            var itemListViewModel = _mapper.Map<ItemListViewModel>(itemListReadModel);

            _logger.LogInformation("Categories count " + itemListReadModel.Categories.Count.ToString());

            return View(itemListViewModel);
        }

        public async Task<IActionResult> GetByCategoryAsync (int id)
        {

            var itemListReadModel = await _itemService.GetItemListOfCategoryId(id);
            var itemListViewModel = _mapper.Map<ItemListViewModel>(itemListReadModel);


            return View("Index", itemListViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var itemModel = await _itemService.GetPopulatedById(id);
            var reviewModelList = await _reviewService.GetByItemIdIncludeUser(id);

            var item = _mapper.Map<ItemApiModel>(itemModel);
            var reviews = _mapper.Map<List<ReviewApiModel>>(reviewModelList);

            //var itemFromDb = await _context.Items.Include(x => x.Category).Include(y => y.Subcategory).Where(x => x.Id == id).FirstOrDefaultAsync();
            //var reviews = _context.Reviews.Include(x => x.ApplicationUser).Where(x => x.ItemId == id).ToList();

            var vm = new ItemDetailsViewModel()
            {
                Item = item,
                ItemId = item.Id,
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
            var userId = IdentityClaimHelper.GetIdFromClaim(User.Identity!);

            if (ModelState.IsValid)
            {
                var command = new AddToCartCommand
                {
                    CartCount = vm.Count,
                    UserId = userId,
                    ItemId = vm.ItemId
                };

                await _cartService.AddItemToCart(command);

                var userCartCount = await _cartService.GetUserCartsCount(userId);

                HttpContext.Session.SetInt32("SessionCart", userCartCount);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
