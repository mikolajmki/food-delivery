using Application.Models;
using Application.Models.ReadModels;
using Domain.Models;

namespace Application.Abstractions;

public interface IModelMapper
{
    public CategoryModel MapToCategoryModel(Category category);
    public ItemModel MapToItemModel(Item item);
    public CouponModel MapToCouponModel(Coupon coupon);
    public ApplicationUserModel MapToApplicationUserModel(ApplicationUserModel user);
    public CartModel MapToCartModel(Cart cart);
    public OrderDetailsReadModel MapToOrderDetailsModel(OrderDetails orderDetails);
    public OrderHeaderModel MapToOrderHeaderModel(OrderHeader orderHeader);
    public ReviewModel MapToReviewModel(Review review);
    public SubcategoryModel MapToSubcategoryModel(Subcategory subcategory);
}
