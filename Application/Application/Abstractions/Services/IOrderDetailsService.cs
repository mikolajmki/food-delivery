using Application.Models;

namespace Application.Abstractions.Services;

public interface IOrderDetailsService : IGenericService<OrderDetailsReadModel>
{
    Task<OrderDetailsReadModel> GetOrderDetails(int orderHeaderId);
}
