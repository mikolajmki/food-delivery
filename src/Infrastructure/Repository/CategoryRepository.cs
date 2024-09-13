using Application.Abstractions.Repositories;
using Domain.Models;
namespace Infrastructure.Repository;

internal class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<Category> GetCategory()
    {
        throw new NotImplementedException();
    }
}
