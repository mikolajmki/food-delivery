using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using MapsterMapper;
using Presentation.ApiModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Principal;
using Presentation.Areas.Identity;

namespace Presentation.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = IdentityClaimHelper.GetIdFromClaim(User.Identity!);
            
            List<ReviewModel> list;

            if (User.IsInRole("Admin")) 
            {
                list = await _reviewService.GetReviewsOfAllUsers();
            } else
            {
                list = await _reviewService.GetReviewsOfUser(userId);
            }

            var reviews = _mapper.Map<List<ReviewApiModel>>(list);

            return View(reviews);
        }

        public async Task<IActionResult> Details(int id)
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
        public async Task<IActionResult> EditAsync (ReviewApiModel review)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<ReviewModel>(review);
                await _reviewService.Update(model.Id, model);

                return RedirectToAction("Index");
            }
            //return RedirectToAction("Details", new { id = review.Id });
            return RedirectToAction("Details");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id) 
        {
            await _reviewService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
