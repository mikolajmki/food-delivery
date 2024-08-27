using Application.Models;

namespace Application.Abstractions.Services;

public interface IOrderDetailsService : IGenericService<OrderDetailsModel>
{
    Task<OrderDetailsModel> GetOrderDetails();
}
