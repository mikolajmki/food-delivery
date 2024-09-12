using Application.Abstractions.Services;
using Application.Models.Commands;
using MapsterMapper;
using Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        [BindProperty]
        public CartOrderViewModel details { get; set; }
        public OrderDetailsViewModel orderDetails { get; set; }

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var cartOrderReadModel = await _cartService.GetCartOfUserIncludeItemsAndOrderTotal(User.Identity!);

            var cartOrderViewModel = _mapper.Map<CartOrderViewModel>(cartOrderReadModel);
    
            return View(cartOrderViewModel);
        }

        public async Task<IActionResult> SummaryAsync()
        {
            var cartOrderReadModel = await _cartService.GetSummary(User.Identity!);
            var cartOrderViewModel = _mapper.Map<CartOrderViewModel>(cartOrderReadModel);

            return View(cartOrderViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SummaryAsync(CartOrderViewModel vm)
        {
            var placeOrderWriteModel = _mapper.Map<PlaceOrderCommand>(vm);

            await _cartService.PlaceOrder(placeOrderWriteModel);

            //HttpContext.Session.SetInt32("SessionCart", 0);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Plus (int id)
        {
            await _cartService.AddToCart(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Minus(int id)
        {
            await _cartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete (int id)
        {
            await _cartService.Delete(id);

            var userCartCount = await _cartService.GetUserCartsCount(User.Identity!);

            //HttpContext.Session.SetInt32("SessionCart", userCartCount);

            return RedirectToAction(nameof(Index));
        }
    }
}
