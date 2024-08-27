using Application.Abstractions.Repositories;
using Domain.Models;

namespace Infrastructure.Repository;

internal class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
{
    public Task<Subcategory> GetSubcategory()
    {
        throw new NotImplementedException();
    }
}
