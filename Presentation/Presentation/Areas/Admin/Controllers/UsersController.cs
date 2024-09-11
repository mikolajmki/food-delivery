using Application.Abstractions.Services;
using MapsterMapper;
using Presentation.ViewModels;
using System.Web.Mvc;

namespace food_delivery.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [System.Web.Mvc.Authorize(Roles = "Admin")]
    public class UsersController : System.Web.Mvc.Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IApplicationUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var list = await _userService.GetAllWithTotalCounts(User.Identity!);
            var users = _mapper.Map<List<UserViewModel>>(list);

            return View(users);
        }
    }
}
