using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IItemRepository : IGenericRepository<Item>
{
    Task<Item> GetItem();
    void GetPopulated();
}
