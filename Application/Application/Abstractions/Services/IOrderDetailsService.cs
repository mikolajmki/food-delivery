using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IOrderDetailsService : IGenericService<OrderDetails>
{
    Task<OrderDetails> GetOrderDetails();
}
