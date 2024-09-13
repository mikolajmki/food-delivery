using Application.Models.ApplicationModels;
using Application.Models.Commands;
using Application.Models.ReadModels;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface ICartService : IGenericService<CartModel, Cart>
{
    Task<bool> AddToCart(int id);
    Task<CartModel> GetCart();
    Task<CartOrderReadModel> GetCartOfUserIncludeItemsAndOrderTotal(string userId);
    Task<CartOrderReadModel> GetSummary(string userId);
    Task<bool> PlaceOrder(PlaceOrderCommand placeOrderWriteModel);
    Task<bool> RemoveFromCart(int id);
    Task<bool> DeleteCartOfUser(string userId);
    Task<int> GetUserCartsCount(string userId);
    Task<bool> AddItemToCart(AddToCartCommand command);
}
