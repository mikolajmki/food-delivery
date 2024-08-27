using Application.Models;
using AutoMapper;
using Presentation.ViewModels;

namespace Presentation.Configuration;

public static class AutoMapperConfiguration
{
    public static MapperConfiguration Create()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<CategoryModel, CategoryViewModel>();
            config.CreateMap<SubcategoryModel, SubcategoryViewModel>();
            config.CreateMap<ItemModel, ItemViewModel>();
            config.CreateMap<CartOrderModel, CartOrderViewModel>();
            config.CreateMap<ItemListModel, ItemListViewModel>();
            config.CreateMap<ItemReviewsModel, ItemReviewsModel>();
            config.CreateMap<OrderDetailsModel, OrderDetailsViewModel>();
            config.CreateMap<ApplicationUserModel, UserViewModel>();
        });
    }
}
