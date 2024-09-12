using Application.Abstractions.Services;
using Application.Models.Commands;
using MapsterMapper;
using Presentation.ViewModels;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ViewResult = System.Web.Mvc.ViewResult;

namespace food_delivery.Areas.Customer.Controllers
{
    [Microsoft.AspNetCore.Mvc.Area("Customer")]
    public class CartController : System.Web.Mvc.Controller
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        [Microsoft.AspNetCore.Mvc.BindProperty]
        public CartOrderViewModel details { get; set; }
        public OrderDetailsViewModel orderDetails { get; set; }

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [System.Web.Mvc.Authorize]
        public async Task<ViewResult> Index()
        {

            var cartOrderReadModel = await _cartService.GetCartOfUserIncludeItemsAndOrderTotal(User.Identity!);

            var cartOrderViewModel = _mapper.Map<CartOrderViewModel>(cartOrderReadModel);
    
            return View(cartOrderViewModel);
        }

        public async Task<ViewResult> SummaryAsync()
        {
            var cartOrderReadModel = await _cartService.GetSummary(User.Identity!);
            var cartOrderViewModel = _mapper.Map<CartOrderViewModel>(cartOrderReadModel);

            return View(cartOrderViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> SummaryAsync(CartOrderViewModel vm)
        {
            var placeOrderWriteModel = _mapper.Map<PlaceOrderCommand>(vm);

            await _cartService.PlaceOrder(placeOrderWriteModel);

            //HttpContext.Session.SetInt32("SessionCart", 0);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Plus (int id)
        {
            await _cartService.AddToCart(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> Minus(int id)
        {
            await _cartService.RemoveFromCart(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete (int id)
        {
            await _cartService.Delete(id);

            var userCartCount = await _cartService.GetUserCartsCount(User.Identity!);

            //HttpContext.Session.SetInt32("SessionCart", userCartCount);

            return RedirectToAction(nameof(Index));
        }
    }
}
