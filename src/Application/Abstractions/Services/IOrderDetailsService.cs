using Application.Models.ReadModels;

namespace Application.Abstractions.Services;

public interface IOrderDetailsService
{
    Task<OrderDetailsReadModel> GetOrderDetailsByOrderHeaderId(int orderHeaderId);
    Task<bool> DeleteOrderDetailsOfOrderHeaderId(int orderHeaderId);
}
