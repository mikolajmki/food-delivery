using Application.Abstractions.Repositories;
using Domain.Models;
using food_delivery.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
{
    private readonly ApplicationDbContext _context;

    public OrderDetailsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderDetails>> GetOrderDetailsOfOrderHeader(int id)
    {
        var list = await _context.OrderDetails
            .Include(x => x.Item)
            .Where(x => x.OrderHeaderId == id)
            .ToListAsync();

        return list;
    }
}
