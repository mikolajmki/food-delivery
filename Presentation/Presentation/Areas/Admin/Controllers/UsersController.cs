using food_delivery.Repository;
using food_delivery.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace food_delivery.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var users = _context.ApplicationUsers
                .Where(x => x.Id != claim.Value)
                .Select(model => new UserViewModel()
                {
                    Name = model.Name,
                    City = model.City,
                    Address = model.Address,
                    Email = model.Email,
                    PostalCode = model.PostalCode,
                    TotalOrders = _context.OrderHeaders.Where(x => x.ApplicationUserId == model.Id).Count(),
                    TotalReviews = _context.Reviews.Where(x => x.ApplicationUserId == model.Id).Count()
                })
                .ToList();

            return View(users);
        }
    }
}
