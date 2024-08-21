using Domain.Models.Abstraction;

namespace Application.Abstractions;

public interface IGenericService<TModel, TEntity> 
    where TModel : class, IBaseEntityModel
    where TEntity : class, IBaseEntity
{
    Task<IQueryable<TModel>> GetAll();
    Task<TModel> GetById(int id);
    Task<bool> Create(TModel model);
    Task<bool> Update(int id, TModel entity);
    Task<bool> Delete(int id);
}
