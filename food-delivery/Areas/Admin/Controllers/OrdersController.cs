using food_delivery.Models;
using food_delivery.Repository;
using food_delivery.Utility;
using food_delivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace food_delivery.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string status)
        {
            if (status == null) status = "all";

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<OrderHeader> orders;

            if (User.IsInRole("Admin"))
            {
                orders = _context.OrderHeaders.Include(x => x.ApplicationUser).ToList();
            }
            else
            {
                orders = _context.OrderHeaders.Where(x => x.ApplicationUserId == claims.Value).ToList();
            }

            orders = orders.OrderByDescending(x => x.OrderDate);

            switch (status)
            {
                case "pending":
                    orders.Where(x => x.OrderStatus == PaymentStatus.StatusPending).ToList();
                    break;
                case "approved":
                    orders.Where(x => x.OrderStatus == PaymentStatus.StatusApproved).ToList();
                    break;
                case "underprocess":
                    orders.Where(x => x.OrderStatus == OrderStatus.StatusInProceess).ToList();
                    break;
                case "shipped":
                    orders.Where(x => x.OrderStatus == OrderStatus.StatusShipped).ToList();
                    break;
            }

            return View(orders);
        }
        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            var vm = new OrderDetailsViewModel()
            {
                OrderHeader = _context.OrderHeaders.Include(x => x.ApplicationUser).FirstOrDefault(x => x.Id == id),
            };

            //ViewBag.PaymentStatus = new SelectList(new List<string> { "Pending", "Approved", "Rejected", "Delay" });
            //ViewBag.OrderStatus = new SelectList(new List<string> {"Pending", "Refunded", "Approved", "Cancelled", "UnderProcess", "Shipped" });

            vm.OrderDetails = _context.OrderDetails.Include(x => x.Item).Where(x => x.OrderHeaderId == vm.OrderHeader.Id).ToList();
            var ids = vm.OrderDetails.Select(x => x.ItemId);
            vm.Reviews = _context.Reviews.Where(x => ids.Contains(x.ItemId) && x.ApplicationUserId == vm.OrderHeader.ApplicationUserId).ToList();

            List<Item> notReviewedItems = new List<Item>();

            foreach (var item in vm.OrderDetails) 
            {
                if (!vm.Reviews.Any(x => x.ItemId == item.ItemId))
                {
                    notReviewedItems.Add(item.Item);
                }
            }

            ViewBag.NotReviewedItems = new SelectList(notReviewedItems, "Id", "Title");

            return View(vm);
        }
        [HttpPost]
        public IActionResult Review(Review newReview) 
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var d = DateTime.Now;
            var date = new DateTime(d.Year, d.Month, d.Day, d.Hour, d.Minute, d.Second);

            newReview.ApplicationUserId = claims.Value;
            newReview.CreatedDate = date;

            _context.Reviews.Add(newReview);
            _context.SaveChanges();

            return RedirectToAction("Index", "Reviews");
        }

        [HttpPost]
        public IActionResult OrderDetails(OrderDetailsViewModel vm)
        {

            OrderDetails orderDetails = _context.OrderDetails.Include(x => x.OrderHeader).Where(x => x.OrderHeader.Id == vm.OrderHeader.Id).FirstOrDefault();

            orderDetails.OrderHeader.PaymentStatus = vm.OrderHeader.PaymentStatus;
            orderDetails.OrderHeader.OrderStatus = vm.OrderHeader.OrderStatus;

            _context.Update(orderDetails);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            OrderHeader orderHeader = _context.OrderHeaders.Where(x => x.Id == id).FirstOrDefault();

            List<OrderDetails> orderDetails = _context.OrderDetails.Where(x => x.OrderHeaderId == id).ToList();

            _context.Remove(orderHeader);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
