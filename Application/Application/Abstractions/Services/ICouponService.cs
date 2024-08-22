using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICouponService : IGenericService<Coupon>
{
    Task<Coupon> GetCoupon();
}
