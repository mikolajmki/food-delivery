using Application.Abstractions;
using Application.Models;
using Domain.Models;

namespace Application.Mappers;

public class ModelMapper : IModelMapper
{
    public ApplicationUserModel MapToApplicationUserModel(ApplicationUserModel user)
    {
        return new ApplicationUserModel
        {
            Name = user.Name,
            City = user.City,
            Address = user.Address,
            PostalCode = user.PostalCode,
        };
    }

    public CartModel MapToCartModel(Cart cart)
    {
        return new CartModel
        {
            Id = cart.Id,
            Count = cart.Count,
        };
    }

    public CategoryModel MapToCategoryModel(Category category)
    {
        return new CategoryModel
        {
            Id = category.Id,
            Title = category.Title,
        };
    }

    public CouponModel MapToCouponModel(Coupon coupon)
    {
        return new CouponModel
        {
            Id = coupon.Id,
            CouponPicture = coupon.CouponPicture,
            Discount = coupon.Discount,
            IsActive = coupon.IsActive,
            MinimumAmount = coupon.MinimumAmount,
            Title = coupon.Title,
            Type = coupon.Type,
        };
    }

    public ItemModel MapToItemModel(Item item)
    {
        return new ItemModel
        {
            Id = item.Id,
            Description = item.Description,
            Image = item.Image,
            Price = item.Price,
            Title = item.Title,
        };
    }

    public OrderDetailsModel MapToOrderDetailsModel(OrderDetails orderDetails)
    {
        return new OrderDetailsModel
        {
            Id = orderDetails.Id,
            Count = orderDetails.Count,
            Name = orderDetails.Name,
        };
    }

    public OrderHeaderModel MapToOrderHeaderModel(OrderHeader orderHeader)
    {
        return new OrderHeaderModel
        {
            Address = orderHeader.Address,
            CouponCode = orderHeader.CouponCode,
            CouponDis = orderHeader.CouponDis,
            Id = orderHeader.Id,
            Name = orderHeader.Name,
            OrderStatus = orderHeader.OrderStatus,
            OrderTotal = orderHeader.OrderTotal,
            PaymentStatus = orderHeader.PaymentStatus,
            Phone = orderHeader.Phone,
            SubTotal = orderHeader.SubTotal,
        };
    }

    public ReviewModel MapToReviewModel(Review review)
    {
        return new ReviewModel
        {
            Id = review.Id,
            Comment = review.Comment,
            CreatedDate = review.CreatedDate,
            Rating = review.Rating,
        };
    }

    public SubcategoryModel MapToSubcategoryModel(Subcategory subcategory)
    {
        return new SubcategoryModel
        {
            Id = subcategory.Id,
            Title = subcategory.Title,
        };
    }
}
