using Application.Abstractions.Repositories;
using Domain.Models;
namespace Infrastructure.Repository;

internal class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public Task<Category> GetCategory()
    {
        throw new NotImplementedException();
    }
}
