using Application.Models.ApplicationModels;
using Domain.Models;
namespace Application.Abstractions.Services;

public interface ICategoryService : IGenericService<CategoryModel, Category>
{
    Task<CategoryModel> GetCategory();
}
