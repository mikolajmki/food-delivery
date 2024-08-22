using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IOrderHeaderRepository : IGenericRepository<OrderHeader>
{
    Task<OrderHeader> GetOrderHeader();
}
