﻿using Application.Abstractions.Services;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjectioncs
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IOrderDetailsService, OrderDetailsService>();
        services.AddScoped<IOrderHeaderService, OrderHeaderService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<ISubcategoryService, SubcategoryService>();

        return services;
    }
}
