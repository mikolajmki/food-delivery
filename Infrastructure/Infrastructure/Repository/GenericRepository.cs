﻿using Application.Abstractions;
using Domain.Models;
using Domain.Models.Abstraction;
using food_delivery.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
    where TEntity : class, IBaseEntity
{
    private readonly ApplicationDbContext _context;

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
        return _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IQueryable<TEntity>> GetAll()
    {
        return _context.Set<TEntity>().AsNoTracking();
    }

    public Task<bool> Update(int id, TEntity entity)
    {
        throw new NotImplementedException();
    }
}
