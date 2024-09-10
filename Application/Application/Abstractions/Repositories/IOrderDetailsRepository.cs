using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IOrderDetailsRepository : IGenericRepository<OrderDetails>
{
    Task<List<OrderDetails>> GetOrderDetailsOfOrderHeader(int id);
}
