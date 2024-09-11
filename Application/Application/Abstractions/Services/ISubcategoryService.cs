using Application.Models.ApplicationModels;

namespace Application.Abstractions.Services;

public interface ISubcategoryService : IGenericService<SubcategoryModel>
{
    Task<List<SubcategoryModel>> GetSubcategoriesOfCategoryId(int id);
    Task<SubcategoryModel> GetFirstSubcategoryOfCategoryId(int id);
    Task<List<SubcategoryModel>> GetAllIncludeCategory();
}
