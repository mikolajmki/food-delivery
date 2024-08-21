using food_delivery.Models;
using food_delivery.Repository;
using food_delivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace food_delivery.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            List<ReviewApiModel> reviews;

            if (User.IsInRole("Admin")) 
            {
                reviews = _context.Reviews.Include(x => x.Item).Include(x => x.ApplicationUser).ToList();
            } else
            {
                reviews = _context.Reviews.Where(x => x.ApplicationUserId == claim.Value).Include(x => x.Item).ToList();
            }

            reviews = reviews.OrderByDescending(x => x.CreatedDate).ToList();

            return View(reviews);
        }

        public IActionResult Details(int id)
        {
            ReviewApiModel review;

            if (User.IsInRole("Admin"))
            {
                 review = _context.Reviews.Include(x => x.Item).Include(x => x.ApplicationUser).FirstOrDefault(x => x.Id == id);
            } else
            {
                review = _context.Reviews.Include(x => x.Item).FirstOrDefault(x => x.Id == id);
            }

            return View(review);
        }

        [HttpPost]
        public IActionResult Edit (ReviewApiModel review)
        {
            ReviewApiModel model = _context.Reviews.FirstOrDefault(x => x.Id == review.Id);

            if (ModelState.IsValid)
            {
                model.Comment = review.Comment;
                model.Rating = review.Rating;

                _context.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Details", new { id = review.Id });
        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var review = _context.Reviews.Where(x => x.Id == id).FirstOrDefault();
            _context.Remove(review);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
