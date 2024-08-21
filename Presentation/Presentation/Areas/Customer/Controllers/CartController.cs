using food_delivery.Models;
using food_delivery.Repository;
using food_delivery.Utility;
using food_delivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace food_delivery.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public CartOrderViewModel details { get; set; }
        public OrderDetailsViewModel orderDetails { get; set; }
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            details = new CartOrderViewModel()
            {
                OrderHeader = new Models.OrderHeaderApiModel(),
            };
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            details.ListOfCart = _context.Carts.Where(x => x.ApplicationUserId == claims.Value).Include(x => x.Item).ToList();

            if (details.ListOfCart != null)
            {
                foreach (var cart in details.ListOfCart)
                {
                    details.OrderHeader.OrderTotal += cart.Item.Price * cart.Count;
                }
                return View(details);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            details = new CartOrderViewModel()
            {
                ListOfCart = _context.Carts.Include(x => x.Item).Where(x => x.ApplicationUserId == claims.Value).ToList(),
                OrderHeader = new Models.OrderHeaderApiModel(),
            };

            details.OrderHeader.ApplicationUser = _context.ApplicationUsers.Where(x => x.Id == claims.Value).FirstOrDefault();
            details.OrderHeader.Name = details.OrderHeader.ApplicationUser.Name;
            details.OrderHeader.Phone = details.OrderHeader.ApplicationUser.PhoneNumber;
            details.OrderHeader.TimeOfPick = DateTime.Now;

            foreach (var cart in details.ListOfCart)
            {
                details.OrderHeader.OrderTotal += (cart.Item.Price) * cart.Count;
            }

            return View(details);
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
