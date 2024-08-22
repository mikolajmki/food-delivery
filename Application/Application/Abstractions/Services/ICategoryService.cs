using Domain.Models;
namespace Application.Abstractions.Repositories;

public interface ICategoryService : IGenericService<Category>
{
    Task<Category> GetCategory();
}
