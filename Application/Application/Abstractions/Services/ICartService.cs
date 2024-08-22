using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICartService : IGenericService<Cart>
{
    Task<Cart> GetCart();
}
