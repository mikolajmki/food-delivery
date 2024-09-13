using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

internal class CouponService : GenericService<CouponModel, Coupon>, ICouponService
{
    private readonly IMapper mapper;

    public CouponService(IGenericRepository<Coupon> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public Task<CouponModel> GetCoupon()
    {
        throw new NotImplementedException();
    }
}
