using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using MapsterMapper;
using Presentation.ApiModels;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;
using HttpGetAttribute = System.Web.Mvc.HttpGetAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;

namespace food_delivery.Areas.Admin.Controllers
{
    [Authorize]
    [RouteArea("Admin")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public async Task<ViewResult> Index()
        {
            List<ReviewModel> list;

            if (User.IsInRole("Admin")) 
            {
                list = await _reviewService.GetReviewsOfAllUsers();
            } else
            {
                list = await _reviewService.GetReviewsOfUser(User.Identity!);
            }

            var reviews = _mapper.Map<List<ReviewApiModel>>(list);

            return View(reviews);
        }

        public async Task<ViewResult> Details(int id)
        {
            ReviewApiModel model;

            if (User.IsInRole("Admin"))
            {
                model = await _reviewService.GetReviewDetailsIncludeUser(id);
            } else
            {
                model = await _reviewService.GetReviewDetailsIncludeUser(id);
            }

            var review = _mapper.Map<ReviewApiModel>(model);

            return View(review);
        }

        [HttpPost]
        public async Task<RedirectToRouteResult> EditAsync (ReviewApiModel review)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<ReviewModel>(review);
                await _reviewService.Update(model.Id, model);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Details", new { id = review.Id });
        }

        [HttpGet]
        public async Task<RedirectToRouteResult> Delete(int id) 
        {
            await _reviewService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
