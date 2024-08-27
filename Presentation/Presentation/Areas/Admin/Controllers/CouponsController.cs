using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models;
using AutoMapper;
using Presentation.ApiModels;
using Presentation.ViewModels;
using System.Web.Mvc;

namespace food_delivery.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
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
        public ActionResult Index()
        {
            var coupons = couponService.GetAll();
            return View(coupons);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CouponViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // file service

                var files = Request.Form.Files;
                byte[] photo = null;
                using (var fileStream = files[0].OpenReadStream())
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
        public async Task<ActionResult> Delete(int id)
        {
            await couponService.Delete(id);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var coupon = await couponService.GetById(id);

            return View(coupon);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(RequestCouponModel request)
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
