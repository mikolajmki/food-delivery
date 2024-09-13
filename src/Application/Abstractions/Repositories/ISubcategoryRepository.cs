using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ISubcategoryRepository : IGenericRepository<Subcategory>
{
    Task<List<Subcategory>> GetAllIncludeCategory();
    Task<Subcategory> GetFirstSubcategoryOfCategoryId(int id);
    Task<List<Subcategory>> GetSubcategoriesOfCategoryId(int id);
}
