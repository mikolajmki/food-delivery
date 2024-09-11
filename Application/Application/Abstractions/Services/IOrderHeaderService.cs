using Application.Models.ApplicationModels;
using Application.Models.Queries;
using Domain.Models;
using System.Security.Principal;

namespace Application.Abstractions.Services;

public interface IOrderHeaderService : IGenericService<OrderHeaderModel>
{
    Task<List<OrderHeaderModel>> GetOrderHeadersOfAllUsers(GetOrderHeadersOfUserQuery query);
    Task<List<OrderHeaderModel>> GetOrderHeadersOfUser(GetOrderHeadersOfUserQuery query);
}
