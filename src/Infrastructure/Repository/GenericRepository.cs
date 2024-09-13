using Application.Abstractions.Repositories;
using Domain.Models.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
    where TEntity : class, IBaseEntity
{
    private readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Create(TEntity model)
    {
        await _context.Set<TEntity>().AddAsync(model);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        TEntity entity = await GetById(id);
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<TEntity> GetById(int id)
    {
        return _context.Set<TEntity>().AsNoTracking().SingleAsync(x => x.Id == id);
    }

    public async Task<IQueryable<TEntity>> GetAll()
    {
        return _context.Set<TEntity>().AsNoTracking();
    }

    public async Task<bool> Update(int id, TEntity entity)
    {
        TEntity model = await _context.Set<TEntity>().SingleAsync(x=> x.Id == id);
        _context.Entry(model).CurrentValues.SetValues(entity);

        return true;
    }
}
