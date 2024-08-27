using Application.Abstractions.Repositories;
using Domain.Models;

namespace Infrastructure.Repository;

internal class CartRepository : GenericRepository<Cart>, ICartRepository
{
    public Task<Cart> GetCart()
    {
        throw new NotImplementedException();
    }
}
