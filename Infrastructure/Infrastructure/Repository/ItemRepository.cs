using Application.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ItemRepository : GenericRepository<Item>, IItemRepository
{
    private readonly ApplicationDbContext _context;

    public ItemRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Item> GetItem()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Item>> GetItemsByCategoryId(int categoryId)
    {
        var list = await _context.Items
            .Include(x => x.Category)
            .Include(x => x.Subcategory)
            .Where(x => x.CategoryId == categoryId)
            .ToListAsync();

        return list;
    }

    public async Task<List<Item>> GetPopulated()
    {
        return await _context.Items
            .Include(x => x.Category)
            .Include(x => x.Subcategory)
            .ToListAsync();
    }

    public async Task<Item> GetPopulatedById(int id)
    {
        return await _context.Items
            .Where(x => x.Id == id)
            .Include(x => x.Category)
            .Include(x => x.Subcategory)
            .SingleAsync();
    }
}
