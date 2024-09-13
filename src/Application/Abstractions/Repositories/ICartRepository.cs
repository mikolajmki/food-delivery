using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ICartRepository : IGenericRepository<Cart>
{
    Task<bool> AddToCart(int id);
    Task<bool> RemoveFromCart(int id);
    Task<Cart> GetCart();
    Task<List<Cart>> GetCartsOfUserIncludeItems(string userId);
    Task<int> GetUserCartsCount(string id);
    Task<Cart> GetCartbyUserIdAndItemId(string userId, int itemId);
}
