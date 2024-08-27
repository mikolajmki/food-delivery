using Application.Models;

namespace Application.Abstractions.Services;

public interface ISubcategoryService : IGenericService<SubcategoryModel>
{
    Task<SubcategoryModel> GetSubcategory();
}
