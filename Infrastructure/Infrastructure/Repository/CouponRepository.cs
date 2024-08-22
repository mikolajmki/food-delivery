﻿using Application.Abstractions.Repositories;
using Domain.Models;

namespace Infrastructure.Repository;

internal class CouponRepository : GenericRepository<Coupon>, CouponService
{
    public Task<Coupon> GetCoupon()
    {
        throw new NotImplementedException();
    }
}