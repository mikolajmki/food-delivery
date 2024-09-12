using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<List<Coupon>> GetActive();
    Task<Coupon> GetCoupon();
}
