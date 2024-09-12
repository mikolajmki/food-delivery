using Application.Models.ApplicationModels;
using Application.Models.Commands;
using Application.Models.ReadModels;
using Domain.Models;
using System.Security.Principal;

namespace Application.Abstractions.Services;

public interface ICartService : IGenericService<CartModel, Cart>
{
    Task<bool> AddToCart(int id);
    Task<CartModel> GetCart();
    Task<CartOrderReadModel> GetCartOfUserIncludeItemsAndOrderTotal(IIdentity identity);
    Task<CartOrderReadModel> GetSummary(IIdentity identity);
    Task<bool> PlaceOrder(PlaceOrderCommand placeOrderWriteModel);
    Task<bool> RemoveFromCart(int id);
    Task<bool> DeleteCartOfUser(IIdentity identity);
    Task<int> GetUserCartsCount(IIdentity identity);
    Task<bool> AddItemToCart(AddToCartCommand command);
}
