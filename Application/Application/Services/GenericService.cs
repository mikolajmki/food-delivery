using Application.Abstractions;
using AutoMapper;
using Domain.Models.Abstraction;

namespace Application.Services;

internal class GenericService<TModel, TEntity> : IGenericService<TModel, TEntity> 
    where TModel : class, IBaseEntityModel
    where TEntity : class, IBaseEntity
{
    private readonly IGenericRepository<TEntity> _repository;
    private readonly MapperConfiguration _mapperConfiguration;

    public GenericService(IGenericRepository<TEntity> repository, MapperConfiguration mapperConfiguration)
    {
        _repository = repository;
        _mapperConfiguration = mapperConfiguration;
    }

    public async Task<bool> Create(TModel model)
    {
        var mapper = new Mapper(_mapperConfiguration);

        TEntity entity = mapper.Map<TEntity>(model);

        await _repository.Create(entity);

        return true;
    }

    public Task<bool> Delete(int id)
    {
        _repository.Delete(id);
    }

    public Task<IQueryable<TModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<TModel> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(int id, TModel entity)
    {
        throw new NotImplementedException();
    }
}
