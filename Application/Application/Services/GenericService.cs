using Application.Abstractions;
using Domain.Models.Abstraction;

namespace Application.Services;

internal class GenericService<TModel, TEntity> : IGenericService<TModel, TEntity> 
    where TModel : class, IBaseEntityModel
    where TEntity : class, IBaseEntity
{
    private readonly IGenericRepository<TEntity> _repository;

    public Task<bool> Create(TModel model)
    {
        
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
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
