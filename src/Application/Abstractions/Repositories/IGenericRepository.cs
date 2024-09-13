using Domain.Models.Abstraction;

namespace Application.Abstractions.Repositories;

public interface IGenericRepository<TEntity>
    where TEntity : class, IBaseEntity
{
    Task<IQueryable<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task<bool> Create(TEntity model);
    Task<bool> Update(int id, TEntity entity);
    Task<bool> Delete(int id);
}
