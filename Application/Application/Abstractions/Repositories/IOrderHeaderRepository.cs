﻿using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IOrderHeaderRepository : IGenericRepository<OrderHeader>
{
    Task<List<OrderHeader>> GetOrderHeadersOfAllUsers();
    Task<List<OrderHeader>> GetOrderHeadersOfUser();
}
