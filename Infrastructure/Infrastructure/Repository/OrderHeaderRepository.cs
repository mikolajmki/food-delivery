using Application.Abstractions.Repositories;
using Domain.Models;

namespace Infrastructure.Repository;

internal class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
{
    public Task<OrderHeader> GetOrderHeader()
    {
        throw new NotImplementedException();
    }
}
