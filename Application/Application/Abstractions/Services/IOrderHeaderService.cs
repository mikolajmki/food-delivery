using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IOrderHeaderService : IGenericService<OrderHeader>
{
    Task<OrderHeader> GetOrderHeader();
}
