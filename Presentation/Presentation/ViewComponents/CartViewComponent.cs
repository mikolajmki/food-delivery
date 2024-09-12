using Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.ViewComponents
{
    public class CartViewComponent: ViewComponent
    {
        private readonly ICartService _cartService;

        public CartViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity!;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claims != null) 
            {
                if (HttpContext.Session.GetInt32("SessionCart") != null)
                {
                    return View(HttpContext.Session.GetInt32("SessionCart"));
                } else
                {
                    HttpContext.Session.SetInt32("SessionCart", await _cartService.GetUserCartsCount(User.Identity!));
                    return View(HttpContext.Session.GetInt32("SessionCart"));
                }
            } else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
