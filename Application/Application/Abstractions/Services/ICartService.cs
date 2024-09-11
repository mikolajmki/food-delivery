using Application.Models.ApplicationModels;

namespace Application.Abstractions.Services;

public interface ICartService : IGenericService<CartModel>
{
    Task<CartModel> GetCart();
}
