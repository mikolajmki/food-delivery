using Application.Abstractions.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

internal class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
{
    private readonly ApplicationDbContext _context;

    public OrderDetailsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> DeleteOrderDetailsOfOrderHeaderId(int id)
    {
        var list = await _context.OrderDetails
            .Where(x => x.OrderHeaderId == id)
            .ToListAsync();

        _context.OrderDetails.RemoveRange(list);

        return true;
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
