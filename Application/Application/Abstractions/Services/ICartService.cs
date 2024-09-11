using Application.Models.ApplicationModels;
using Application.Models.ReadModels;
using System.Security.Principal;

namespace Application.Abstractions.Services;

public interface ICartService : IGenericService<CartModel>
{
    Task<CartModel> GetCart();
    Task<CartOrderReadModel> GetCartOfUserIncludeItemsAndOrderTotal(IIdentity identity);
    Task<CartOrderReadModel> GetSummary(IIdentity identity);
}
