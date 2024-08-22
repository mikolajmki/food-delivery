using Application.Abstractions.Repositories;
using System.Web.Mvc;

namespace food_delivery.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [Authorize(Roles = "Admin")]
    public class CouponsController : Controller
    {
        private readonly IGenericRepository<Coupon> _couponRepository;

        public CouponsController(IGenericRepository<Coupon> couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var coupons = _couponRepository.GetAll();
            return View(coupons);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Coupon coupons)
        {
            if (ModelState.IsValid)
            {
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
                coupons.CouponPicture = photo;
                _couponRepository.Create(coupons);
                return RedirectToAction("Index");
            }
            return View(coupons);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var coupon = _context.Coupons.Where(x => x.Id == id).FirstOrDefault();
            if (coupon == null)
            {
                return NotFound();
            }
            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var coupon = _context.Coupons.Where(x => x.Id == id).FirstOrDefault();
            if (coupon == null)
            {
                return NotFound();
            }
            return View(coupon);
        }
        [HttpPost]
        public IActionResult Edit(Coupon model)
        {
            var coupon = _context.Coupons.Where(x => x.Id == model.Id).FirstOrDefault();
            if (ModelState.IsValid)
            {
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] photo = null;
                    using (var fileStream = files[0].OpenReadStream())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            fileStream.CopyTo(memoryStream);
                            photo = memoryStream.ToArray();
                        }
                    }
                    coupon.CouponPicture = photo;
                }
                coupon.MinimumAmount = model.MinimumAmount;
                coupon.Discount = model.Discount;
                coupon.IsActive = model.IsActive;
                coupon.Title = model.Title;
                coupon.Type = model.Type;

                _context.Coupons.Update(coupon);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
