using Application.Models;

namespace Application.Abstractions.Services;

public interface IItemService : IGenericService<ItemModel>
{
    Task<ItemModel> GetItem();
    void GetPopulated();
}
