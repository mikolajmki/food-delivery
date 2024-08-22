using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface ItemService : IGenericService<Item>
{
    Task<Item> GetItem();
    void GetPopulated();
}
