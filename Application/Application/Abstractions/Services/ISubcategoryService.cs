using Application.Models.ApplicationModels;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface ISubcategoryService : IGenericService<SubcategoryModel, Subcategory>
{
    Task<List<SubcategoryModel>> GetSubcategoriesOfCategoryId(int id);
    Task<SubcategoryModel> GetFirstSubcategoryOfCategoryId(int id);
    Task<List<SubcategoryModel>> GetAllIncludeCategory();
}
