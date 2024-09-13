using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

internal class ItemService : GenericService<ItemModel, Item>, IItemService
{
    private readonly IItemRepository _repository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICouponRepository _couponRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;

    public ItemService(
        IGenericRepository<Item> repository,
        IMapper mapper,
        ICategoryRepository categoryRepository,
        ICouponRepository couponRepository,
        IReviewRepository reviewRepository,
        IItemRepository itemRepository

    ) : base(repository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _couponRepository = couponRepository;
        _reviewRepository = reviewRepository;
        _itemRepository = itemRepository;
    }

    public Task<ItemModel> GetItem()
    {
        throw new NotImplementedException();
    }

    public async Task<ItemListReadModel> GetItemList()
    {
        var dbCategories = await _categoryRepository.GetAll();
        var dbCoupons = await _couponRepository.GetActive();
        var dbItems = await _itemRepository.GetPopulated();

        var items = _mapper.Map<List<ItemModel>>(dbItems);
        var categories = _mapper.Map<List<CategoryModel>>(dbCategories);
        var coupons = _mapper.Map<List<CouponModel>>(dbCoupons);

        items = await CalculateTotalAndAvgAsync(items);

        var read = new ItemListReadModel
        {
            Categories = categories,
            CategoriesList = categories,
            Coupons = coupons,
            Items = items,
        };

        return read;
    }

    public async Task<ItemListReadModel> GetItemListOfCategoryId(int categoryId)
    {
        var dbCategories = await _categoryRepository.GetAll();
        var dbCoupons = await _couponRepository.GetActive();
        var dbItems = await _itemRepository.GetItemsByCategoryId(categoryId);

        var items = _mapper.Map<List<ItemModel>>(dbItems);
        var categories = _mapper.Map<List<CategoryModel>>(dbCategories);
        var coupons = _mapper.Map<List<CouponModel>>(dbCoupons);

        items = await CalculateTotalAndAvgAsync(items);

        var read = new ItemListReadModel
        {
            Categories = categories,
            CategoriesList = categories,
            Coupons = coupons,
            Items = items,
        };

        return read;
    }

    private async Task<List<ItemModel>> CalculateTotalAndAvgAsync(List<ItemModel> items)
    {
        foreach (var item in items)
        {
            var reviews = await _reviewRepository.GetReviewsOfItem(item.Id);

            item.TotalReviewsCount = reviews.Count;

            if (item.TotalReviewsCount > 0)
            {
                var avg = (double)reviews.Sum(x => x.Rating) / (double)item.TotalReviewsCount;
                item.AverageRating = avg;
            }
            else
            {
                item.AverageRating = 0;
            }
        }

        return items;
    }

    public async Task<List<ItemModel>> GetPopulated()
    {
        var list = await _repository.GetPopulated();
        var items = _mapper.Map<List<ItemModel>>(list);

        return items;
    }

    public async Task<ItemModel> GetPopulatedById(int id)
    {
        var entity = await _repository.GetPopulatedById(id);
        var item = _mapper.Map<ItemModel>(entity);

        return item;
    }
}
