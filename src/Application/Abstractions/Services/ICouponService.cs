using Application.Models.ApplicationModels;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface ICouponService : IGenericService<CouponModel, Coupon>
{
    Task<CouponModel> GetCoupon();
}
