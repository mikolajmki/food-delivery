using Application.Abstractions.Services;
using food_delivery.Utility;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels;
using Presentation.ViewModels;
using System.Security.Claims;
using System.Web.Mvc;

namespace food_delivery.Areas.Customer.Controllers
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
        public IActionResult Summary(CartOrderViewModel vm)
        {
            OrderHeaderApiModel orderHeader = new Models.OrderHeaderApiModel()
            {
                ApplicationUserId = vm.OrderHeader.ApplicationUser.Id,
                Name = vm.OrderHeader.Name,
                Phone = vm.OrderHeader.Phone,
                Address = vm.OrderHeader.Address,
                OrderDate = vm.OrderHeader.OrderDate,
                TimeOfPick = vm.OrderHeader.TimeOfPick,
                DateOfPick = vm.OrderHeader.DateOfPick,
                OrderTotal = vm.OrderHeader.OrderTotal,
                OrderStatus = OrderStatus.StatusPending,
                PaymentStatus = PaymentStatus.StatusPending
            };

            _context.Add(orderHeader);
            _context.SaveChanges();

            var carts = _context.Carts.Where(x => x.ApplicationUserId == vm.OrderHeader.ApplicationUser.Id).Include(x => x.Item).ToList();

            foreach (var cart in carts)
            {
                OrderDetailsApiModel orderDetails = new Models.OrderDetailsApiModel()
                {
                    OrderHeaderId = orderHeader.Id,
                    ItemId = cart.Item.Id,
                    Name = orderHeader.Name,
                    Count = cart.Count
                };
                _context.Add(orderDetails);
                _context.Remove(cart);
            }

            HttpContext.Session.SetInt32("SessionCart", 0);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Plus (int id)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            cart.Count += 1;
            HttpContext.Session.SetInt32("SessionCart", cart.Count);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Minus(int id)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cart.Count == 1)
            {
                _context.Carts.Remove(cart);
                _context.SaveChanges();
            }
            cart.Count -= 1;
            HttpContext.Session.SetInt32("SessionCart", cart.Count);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete (int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            _context.Carts.Remove(cart);
            _context.SaveChanges();

            var userCartCount = _context.Carts.Where(x => x.ApplicationUserId == claims.Value).ToList().Count();
            HttpContext.Session.SetInt32("SessionCart", userCartCount);

            return RedirectToAction(nameof(Index));
        }
    }
}
