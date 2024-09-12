using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Application.Models.Options;
using Application.Models.Queries;
using MapsterMapper;
using Presentation.ApiModels;
using Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Presentation.Areas.Identity;

namespace Presentation.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderDetailsService _orderDetailsService;
        private readonly IOrderHeaderService _orderHeaderService;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public OrdersController(
            IOrderDetailsService orderDetailsService,
            IOrderHeaderService orderHeaderService,
            IReviewService reviewService)
        {
            _orderDetailsService = orderDetailsService;
            _orderHeaderService = orderHeaderService;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> IndexAsync(OrderHeaderOrderByApiOptions options)
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
        public async Task<IActionResult> OrderDetailsAsync(int id)
        {
            var orderDetailsReadModel = await _orderDetailsService.GetOrderDetailsByOrderHeaderId(id);

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
        public async Task<IActionResult> Review(ReviewApiModel review) 
        {
            var userId = IdentityClaimHelper.GetIdFromClaim(User.Identity!);
            var model = _mapper.Map<ReviewModel>(review);

            await _reviewService.CreateReview(model, userId);

            //return RedirectToAction("Index", "Reviews");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult OrderDetails(OrderDetailsViewModel vm)
        {
            var orderHeader = _mapper.Map<OrderHeaderModel>(vm.OrderHeader);

            _orderHeaderService.Update(orderHeader.Id, orderHeader);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _orderDetailsService.DeleteOrderDetailsOfOrderHeaderId(id);

            return RedirectToAction("Index");
        }
    }
}
