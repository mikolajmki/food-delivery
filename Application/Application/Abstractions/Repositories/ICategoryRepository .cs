using Domain.Models;
namespace Application.Abstractions.Repositories;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category> GetCategory();
}
