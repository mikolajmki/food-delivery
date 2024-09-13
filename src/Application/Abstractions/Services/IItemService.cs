using Application.Models.ApplicationModels;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface IItemService : IGenericService<ItemModel, Item>
{
    Task<ItemModel> GetItem();
    Task<ItemListReadModel> GetItemList();
    Task<ItemListReadModel> GetItemListOfCategoryId(int categoryId);
    Task<List<ItemModel>> GetPopulated();
    Task<ItemModel> GetPopulatedById(int id);
}
