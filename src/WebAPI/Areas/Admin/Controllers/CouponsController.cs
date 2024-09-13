using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiModels;
using Presentation.ViewModels;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CouponsController : Controller
    {
        private readonly ICouponService couponService;
        private readonly IMapper mapper;

        public CouponsController(ICouponService couponService, IMapper mapper)
        {
            this.couponService = couponService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = couponService.GetAll();
            var coupons = mapper.Map<List<CouponViewModel>>(list);

            return View(coupons);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CouponViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // file service
                byte[] photo = null;
                using (var fileStream = file.OpenReadStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        photo = memoryStream.ToArray();
                    }
                }

                vm.CouponPicture = photo;

                ///

                var coupon = mapper.Map<CouponModel>(vm);

                await couponService.Create(coupon);


                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await couponService.Delete(id);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var coupon = await couponService.GetById(id);

            return View(coupon);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RequestCouponModel request)
        {
            if (ModelState.IsValid)
            {

                // file service

                var file = request.file;

                byte[] photo = null;

                using (var fileStream = file.OpenReadStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        photo = memoryStream.ToArray();
                    }
                }
                request.CouponPicture = photo;
                //

                var coupon = mapper.Map<CouponModel>(request);
                await couponService.Update(coupon.Id, coupon);

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
