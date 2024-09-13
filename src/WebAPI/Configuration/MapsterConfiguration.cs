using Application.Models.ApplicationModels;
using Application.Models.ReadModels;
using Mapster;
using MapsterMapper;
using Presentation.ViewModels;

namespace WebApi.Configuration;

public static class MapsterConfiguration
{
    public static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();

        config.NewConfig<CategoryModel, CategoryViewModel>();
        config.NewConfig<SubcategoryModel, SubcategoryViewModel>();
        config.NewConfig<ItemModel, ItemViewModel>();
        config.NewConfig<CartOrderModel, CartOrderViewModel>();
        config.NewConfig<ItemListReadModel, ItemListViewModel>();
        config.NewConfig<ItemReviewsModel, ItemReviewsViewModel>();
        config.NewConfig<OrderDetailsReadModel, OrderDetailsViewModel>();
        config.NewConfig<ApplicationUserModel, UserViewModel>();

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
