﻿using Application.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetByItemIdIncludeUser(int id)
    {
        var items = await _context.Reviews
            .Include(x => x.ApplicationUser)
            .Where(x => x.ItemId == id)
            .ToListAsync();

        return items;
    }

    public async Task<Review> GetReviewDetails(int id)
    {
        var review = await _context.Reviews
            .Include(x => x.Item)
            .SingleAsync(x => x.Id == id);

        return review;
    }

    public async Task<Review> GetReviewDetailsIncludeUser(int id)
    {
        var review = await _context.Reviews
            .Include(x => x.Item)
            .Include(x => x.ApplicationUser)
            .SingleAsync(x => x.Id == id);

        return review;
    }

    public async Task<List<Review>> GetReviewsOfAllUsers()
    {
        var reviews = await _context.Reviews
            .Include(x => x.Item)
            .Include(x => x.ApplicationUser)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();

        return reviews;
    }

    public async Task<List<Review>> GetReviewsOfItem(int itemId)
    {
        var reviews = await _context.Reviews
            .Where(x => x.ItemId == itemId)
            .ToListAsync();

        return reviews;
    }

    public async Task<List<Review>> GetReviewsOfItemsByUser(List<int> ids, string userId)
    {
        var list = await _context.Reviews
            .Where(x => ids.Contains(x.ItemId) && x.ApplicationUserId == userId)
            .ToListAsync();

        return list;
    }

    public async Task<List<Review>> GetReviewsOfUser(string id)
    {
        var reviews = await _context.Reviews
            .Where(x => x.ApplicationUserId == id)
            .Include(x => x.Item)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();

        return reviews;
    }
}
