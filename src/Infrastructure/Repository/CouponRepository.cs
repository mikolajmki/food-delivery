using Application.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
    private readonly ApplicationDbContext _context;

    public CouponRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Coupon> GetCoupon()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Coupon>> GetActive()
    {
        var coupons = await _context.Coupons
            .Where(c => c.IsActive)
            .ToListAsync();

        return coupons;
    }
}
