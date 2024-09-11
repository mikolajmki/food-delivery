using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICartRepository : IGenericRepository<Cart>
{
    Task<Cart> GetCart();
    Task<List<Cart>> GetCartsOfUserIncludeItems(string userId);
}
