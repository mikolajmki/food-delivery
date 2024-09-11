using Application.Models.ApplicationModels;

namespace Application.Abstractions.Services;

public interface IItemService : IGenericService<ItemModel>
{
    Task<ItemModel> GetItem();
    Task<List<ItemModel>> GetPopulated();
}
