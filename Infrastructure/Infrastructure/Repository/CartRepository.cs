using Application.Abstractions.Repositories;
using Domain.Models;
using food_delivery.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Repository;

internal class CartRepository : GenericRepository<Cart>, ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Cart> GetCart()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Cart>> GetCartsOfUserIncludeItems(string userId)
    {
        var carts = await _context.Carts
            .Include(x => x.Item)
            .Where(x => x.ApplicationUserId == userId)
            .ToListAsync();

        return carts;
    }
}
