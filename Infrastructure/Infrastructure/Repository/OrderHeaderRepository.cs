using Application.Abstractions.Repositories;
using Domain.Models;
using food_delivery.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderHeaderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderHeader> GetOrderHeaderByIdIncludeUser(int id)
    {
        var orderheader = await _context.OrderHeaders
            .Include(x => x.ApplicationUser)
            .SingleAsync(x => x.Id == id);

        return orderheader;
    }

    public async Task<List<OrderHeader>> GetOrderHeadersOfAllUsers()
    {
        var orderHeaders = await _context.OrderHeaders
            .Include(x => x.ApplicationUser)
            .ToListAsync();

        return orderHeaders;
    }

    public Task<List<OrderHeader>> GetOrderHeadersOfUser(string id)
    {
        var orderHeaders = _context.OrderHeaders
            .Where(x => x.ApplicationUserId == id.ToString())
            .ToListAsync();

        return orderHeaders;
    }
}
