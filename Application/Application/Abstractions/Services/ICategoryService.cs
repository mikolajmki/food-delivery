using Application.Models.ApplicationModels;
namespace Application.Abstractions.Services;

public interface ICategoryService : IGenericService<CategoryModel>
{
    Task<CategoryModel> GetCategory();
}
