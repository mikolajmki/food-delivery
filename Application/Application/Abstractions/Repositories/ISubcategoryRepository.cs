using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ISubcategoryRepository : IGenericRepository<Subcategory>
{
    Task<Subcategory> GetSubcategory();
}
