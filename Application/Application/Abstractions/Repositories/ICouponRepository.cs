using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<Coupon> GetCoupon();
}
