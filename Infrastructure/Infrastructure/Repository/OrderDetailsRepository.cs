using Application.Abstractions.Repositories;
using Domain.Models;

namespace Infrastructure.Repository;

internal class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
{
    public Task<OrderDetails> GetOrderDetails()
    {
        throw new NotImplementedException();
    }
}
