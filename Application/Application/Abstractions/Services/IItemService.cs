using Application.Models.ApplicationModels;

namespace Application.Abstractions.Services;

public interface IItemService : IGenericService<ItemModel>
{
    Task<ItemModel> GetItem();
    Task<ItemListReadModel> GetItemList();
    Task<ItemListReadModel> GetItemListOfCategoryId(int categoryId);
    Task<List<ItemModel>> GetPopulated();
    Task<ItemModel> GetPopulatedById(int id);
}
