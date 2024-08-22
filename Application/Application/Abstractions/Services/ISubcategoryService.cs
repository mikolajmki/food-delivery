using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ISubcategoryService : IGenericService<Subcategory>
{
    Task<Subcategory> GetSubcategory();
}
