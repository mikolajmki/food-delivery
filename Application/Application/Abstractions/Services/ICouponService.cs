using Application.Models;

namespace Application.Abstractions.Services;

public interface ICouponService : IGenericService<CouponModel>
{
    Task<CouponModel> GetCoupon();
}
