using Application.Abstractions.Services;
using MapsterMapper;
using Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IApplicationUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IApplicationUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _userService.GetAllWithTotalCounts(User.Identity!);
            var users = _mapper.Map<List<UserViewModel>>(list);

            return View(users);
        }
    }
}
