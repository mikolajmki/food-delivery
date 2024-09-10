using Application.Models;

namespace Application.Abstractions.Services;

public interface ISubcategoryService : IGenericService<SubcategoryModel>
{
    Task<List<SubcategoryModel>> GetSubcategoriesOfCategoryId(int id);
}
