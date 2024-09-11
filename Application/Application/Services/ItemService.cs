using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

internal class ItemService : GenericService<ItemModel, Item>, IItemService
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public ItemService(IGenericRepository<Item> repository, IMapper mapper) : base(repository)
    {
        _mapper = mapper;
    }

    public Task<ItemModel> GetItem()
    {
        throw new NotImplementedException();
    }

    public async Task<List<ItemModel>> GetPopulated()
    {
        var list = await _repository.GetPopulated();
        var items = _mapper.Map<List<ItemModel>>(list);

        return items;
    }
}
