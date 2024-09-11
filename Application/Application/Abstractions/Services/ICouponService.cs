using Application.Models.ApplicationModels;

namespace Application.Abstractions.Services;

public interface ICouponService : IGenericService<CouponModel>
{
    Task<CouponModel> GetCoupon();
}
