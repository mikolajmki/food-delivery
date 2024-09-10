using Application.Abstractions.Services;
using Application.Models;
using Application.Models.Options;
using Application.Models.Queries;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels;
using Presentation.ViewModels;
using System.Security.Claims;
using System.Web.Mvc;
using ActionResult = System.Web.Mvc.ActionResult;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;

namespace food_delivery.Areas.Admin.Controllers
{
    [System.Web.Mvc.Authorize]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrderDetailsService orderDetailsService,
            IOrderHeaderService orderHeaderService
        )
        {
            _orderDetailsService = orderDetailsService;
            _orderHeaderService = orderHeaderService;
        }

        public async Task<ActionResult> IndexAsync(OrderHeaderOrderByApiOptions options)
        {
            var orderby = _mapper.Map<OrderHeaderOrderByOptions>(options);

            var query = new GetOrderHeadersOfUserQuery(orderby);

            List<OrderHeaderModel> orders;

            if (User.IsInRole("Admin"))
            {
                orders = await _orderHeaderService.GetOrderHeadersOfAllUsers(query);
            }
            else
            {
                query.Identity = User.Identity;
                orders = await _orderHeaderService.GetOrderHeadersOfUser(query);
            }

            return View(orders);
        }
        [HttpGet]
        public async Task<ActionResult> OrderDetailsAsync(int id)
        {
            var orderDetailsReadModel = await _orderDetailsService.GetOrderDetails(id);

            List<ItemApiModel> notReviewedItems = new List<ItemApiModel>();

            foreach (var item in orderDetailsReadModel.OrderDetails) 
            {
                if (!orderDetailsReadModel.Reviews.Any(x => x.ItemId == item.ItemId))
                {
                    //notReviewedItems.Add(item.Item);
                }
            }

            ViewBag.NotReviewedItems = new SelectList(notReviewedItems, "Id", "Title");

            var vm = _mapper.Map<OrderDetailsViewModel>(orderDetailsReadModel);

            return View(vm);
        }
        [HttpPost]
        public IActionResult Review(ReviewApiModel newReview) 
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

            OrderDetailsApiModel orderDetails = _context.OrderDetails.Include(x => x.OrderHeader).Where(x => x.OrderHeader.Id == vm.OrderHeader.Id).FirstOrDefault();

            orderDetails.OrderHeader.PaymentStatus = vm.OrderHeader.PaymentStatus;
            orderDetails.OrderHeader.OrderStatus = vm.OrderHeader.OrderStatus;

            _context.Update(orderDetails);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            OrderHeaderApiModel orderHeader = _context.OrderHeaders.Where(x => x.Id == id).FirstOrDefault();

            List<OrderDetailsApiModel> orderDetails = _context.OrderDetails.Where(x => x.OrderHeaderId == id).ToList();

            _context.Remove(orderHeader);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
