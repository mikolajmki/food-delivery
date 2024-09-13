using Application.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
{
    private readonly ApplicationDbContext _context;

    public SubcategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Subcategory>> GetAllIncludeCategory()
    {
        var subcategory = await _context.Subcategories
            .Include(x => x.Category)
            .ToListAsync();

        return subcategory;
    }

    public async Task<Subcategory> GetFirstSubcategoryOfCategoryId(int id)
    {
        var subcategory = await _context.Subcategories
            .Where(x => x.CategoryId == id)
            .FirstAsync();

        return subcategory;
    }

    public async Task<List<Subcategory>> GetSubcategoriesOfCategoryId(int id)
    {
        var subcategories = await _context.Subcategories
            .Where(x => x.CategoryId == id)
            .ToListAsync();

        return subcategories;
    }
}
