using Application.Models;

namespace Application.Abstractions.Services;

public interface ICartService : IGenericService<CartModel>
{
    Task<CartModel> GetCart();
}
