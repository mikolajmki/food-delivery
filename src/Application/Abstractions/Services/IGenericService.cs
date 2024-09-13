using Domain.Models.Abstraction;

namespace Application.Abstractions.Services;

public interface IGenericService<TModel, TEntity>
    where TModel : class, IBaseEntityModel
{
    Task<IQueryable<TModel>> GetAll();
    Task<TModel> GetById(int id);
    Task<bool> Create(TModel model);
    Task<bool> Update(int id, TModel model);
    Task<bool> Delete(int id);
}
