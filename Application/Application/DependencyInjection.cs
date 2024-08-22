using Application.Models;
using AutoMapper;
using food_delivery.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped(service => new MapperConfiguration(config =>
        {
            config.CreateMap<Category, CategoryModel>();
            config.CreateMap<Subcategory, SubcategoryModel>();
            config.CreateMap<Item, ItemModel>();
            config.CreateMap<Review, ReviewModel>();
            config.CreateMap<Cart, CartModel>();
            config.CreateMap<Coupon, CouponModel>();
            config.CreateMap<OrderDetails, OrderDetailsModel>();
            config.CreateMap<OrderHeader, OrderHeaderModel>();
        }));
        return services;
    }
}
