using Application.Abstractions.Repositories;
using Domain.Models;
using food_delivery.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
{
    private readonly ApplicationDbContext _context;

    public SubcategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Subcategory>> GetSubcategoryOfCategoryId(int id)
    {
        var subcategories = await _context.Subcategories
            .Where(x => x.CategoryId == id)
            .ToListAsync();

        return subcategories;
    }
}
