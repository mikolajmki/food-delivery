using Application.Abstractions;
using Domain.Models;
using food_delivery.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Review>> GetReviewsOfItemsByUser(List<int> ids, string userId)
    {
        var list = await _context.Reviews
            .Where(x => ids.Contains(x.ItemId) && x.ApplicationUserId == userId)
            .ToListAsync();

        return list;
    }
}
