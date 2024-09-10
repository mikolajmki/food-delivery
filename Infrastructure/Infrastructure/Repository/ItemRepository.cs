using Application.Abstractions.Repositories;
using Domain.Models;
using food_delivery.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    private readonly ApplicationDbContext _context;

    public Task<Item> GetItem()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Item>> GetPopulated()
    {
        return await _context.Items
            .Include(x => x.Category)
            .Include(x => x.Subcategory)
            .ToListAsync();
    }
}
