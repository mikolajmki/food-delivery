using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<Item> GetItem();
    Task<List<Item>> GetItemsByCategoryId(int categoryId);
    Task<List<Item>> GetPopulated();
    Task<Item> GetPopulatedById(int id);
}
