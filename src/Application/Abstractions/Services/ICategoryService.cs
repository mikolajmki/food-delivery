using Application.Models.ApplicationModels;
<<<<<<< HEAD:Application/Application/Abstractions/Services/ICategoryService.cs
=======
using Domain.Models;
>>>>>>> develop:src/Application/Abstractions/Services/ICategoryService.cs
namespace Application.Abstractions.Services;

public interface ICategoryService : IGenericService<CategoryModel, Category>
{
    Task<CategoryModel> GetCategory();
}
