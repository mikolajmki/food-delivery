using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models;
using AutoMapper;
using Domain.Models;
using Domain.Models.Abstraction;

namespace Application.Services;

public class GenericService<TModel, TEntity> : IGenericService<TModel> 
    where TModel : class, IBaseEntityModel
    where TEntity : class, IBaseEntity
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IGenericRepository<TEntity> repository)
    {
        var configuration = CreateMapperConfiguration();

        _repository = repository;
        _mapper = configuration.CreateMapper();
    }

    public async Task<bool> Create(TModel model)
    {
        TEntity entity = _mapper.Map<TEntity>(model);
        await _repository.Create(entity);
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        await _repository.Delete(id);
        return true;
    }

    public async Task<IQueryable<TModel>> GetAll()
    {
        var all = await _repository.GetAll();
        var allModels = all.Select(x => _mapper.Map<TModel>(x));
        return allModels;
    }

    public async Task<TModel> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        var model = _mapper.Map<TModel>(entity);
        return model;
    }

    public async Task<bool> Update(int id, TModel model)
    {
        var entity = _mapper.Map<TEntity>(model);
        await _repository.Update(id, entity);
        return true;
    }

    private static MapperConfiguration CreateMapperConfiguration()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<Category, CategoryModel>();
            config.CreateMap<Item, ItemModel>();
            config.CreateMap<Subcategory, SubcategoryModel>();
            config.CreateMap<Review, ReviewModel>();
            config.CreateMap<Cart, CartModel>();
            config.CreateMap<Coupon, CouponModel>();
            config.CreateMap<OrderDetails, OrderDetailsModel>();
            config.CreateMap<OrderHeader, OrderHeaderModel>();
        });
    }
}
