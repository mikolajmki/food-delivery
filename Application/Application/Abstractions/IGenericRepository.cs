using Domain.Models;
using Domain.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions;

public interface IGenericRepository<TEntity> 
    where TEntity : class, IBaseEntity
{
    Task<IQueryable<TEntity>> GetAll();
    Task<TEntity> GetById(int id);
    Task<bool> Create(TEntity model);
    Task<bool> Update(int id, TEntity entity);
    Task<bool> Delete(int id);
}
