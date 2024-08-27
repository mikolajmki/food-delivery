using Application.Models;

namespace Application.Abstractions.Services;

public interface IOrderHeaderService : IGenericService<OrderHeaderModel>
{
    Task<OrderHeaderModel> GetOrderHeader();
}
