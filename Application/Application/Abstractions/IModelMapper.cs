﻿using Application.Models;
using food_delivery.Models;

namespace Application.Abstractions;

public interface IModelMapper
{
    public CategoryModel MapToCategoryModel(Category category);
    public ItemModel MapToItemModel(Item item);
    public CouponModel MapToCouponModel(Coupon coupon);
    public ApplicationUserModel MapToApplicationUserModel(ApplicationUserModel user);
    public CartModel MapToCartModel(Cart cart);
    public OrderDetailsModel MapToOrderDetailsModel(OrderDetails orderDetails);
    public OrderHeaderModel MapToOrderHeaderModel(OrderHeader orderHeader);
    public ReviewModel MapToReviewModel(Review review);
    public SubcategoryModel MapToSubcategoryModel(Subcategory subcategory);
}