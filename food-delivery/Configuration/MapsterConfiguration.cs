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
        //return new MapperConfiguration(config =>
        //{
        //    config.CreateMap<CategoryModel, CategoryViewModel>();
        //    config.CreateMap<SubcategoryModel, SubcategoryViewModel>();
        //    config.CreateMap<ItemModel, ItemViewModel>();
        //    config.CreateMap<CartOrderModel, CartOrderViewModel>();
        //    config.CreateMap<ItemListModel, ItemListViewModel>();
        //    config.CreateMap<ItemReviewsModel, ItemReviewsModel>();
        //    config.CreateMap<OrderDetailsModel, OrderDetailsViewModel>();
        //    config.CreateMap<ApplicationUserModel, UserViewModel>();
        //});
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
