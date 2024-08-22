using Application.Abstractions.Repositories;
using Domain.Models;

namespace Infrastructure.Repository;

internal class OrderHeaderRepository : GenericRepository<OrderHeader>, OrderHeaderService
{
    public Task<OrderHeader> GetOrderHeader()
    {
        throw new NotImplementedException();
    }
}
