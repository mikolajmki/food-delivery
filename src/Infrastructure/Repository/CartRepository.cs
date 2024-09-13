using Application.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class CartRepository : GenericRepository<Cart>, ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> AddToCart(int id)
    {
        var cart = await _context.Carts.FirstAsync(c => c.Id == id);
        cart.Count += 1;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RemoveFromCart(int id)
    {
        var cart = await _context.Carts.FirstAsync(c => c.Id == id);
        cart.Count += 1;

        if (cart.Count == 1)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        await _context.SaveChangesAsync();

        return true;
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

    public async Task<int> GetUserCartsCount(string id)
    {
        var count = await _context.Carts
            .Where(x => x.ApplicationUserId == id)
            .CountAsync();

        return count;
    }

    public async Task<Cart> GetCartbyUserIdAndItemId(string userId, int itemId)
    {
        var cart = await _context.Carts
            .Where(x => 
                x.ApplicationUserId == userId && 
                x.ItemId == itemId
            ).SingleAsync();

        return cart;
    }
}
