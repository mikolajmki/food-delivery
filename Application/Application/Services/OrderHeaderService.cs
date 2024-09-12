using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Application.Models.Options;
using Application.Models.Queries;
using Domain.Models;
using Domain.Utility;
using MapsterMapper;

namespace Application.Services;

internal class OrderHeaderService : GenericService<OrderHeaderModel, OrderHeader>, IOrderHeaderService
{
    private readonly IOrderHeaderRepository _orderHeaderRepository;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public OrderHeaderService(
        IGenericRepository<OrderHeader> repository,
        IOrderHeaderRepository orderHeaderRepository,
        IMapper mapper) : base(repository)
    {
        _orderHeaderRepository = orderHeaderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderHeaderModel>> GetOrderHeadersOfAllUsers(GetOrderHeadersOfUserQuery query)
    {
        var list = await _orderHeaderRepository.GetOrderHeadersOfAllUsers();
        var orders = _mapper.Map<List<OrderHeaderModel>>(list);

        orders = OrderBy(query.Options, orders);

        return orders;
    }

    public async Task<List<OrderHeaderModel>> GetOrderHeadersOfUser(GetOrderHeadersOfUserQuery query)
    {
        var id = _identityService.GetIdFromClaim(query.Identity);
        var list = await _orderHeaderRepository.GetOrderHeadersOfUser(id);

        var orders = _mapper.Map<List<OrderHeaderModel>>(list);

        orders = OrderBy(query.Options, orders);

        return orders;
    }

    private static List<OrderHeaderModel> OrderBy(
        OrderHeaderOrderByOptions options, 
        List<OrderHeaderModel> orders)
    {

        var linq = orders.OrderByDescending(x => x.OrderDate).AsQueryable();

        switch (options)
        {
            case OrderHeaderOrderByOptions.Pending:
                linq = linq.Where(x => x.OrderStatus == PaymentStatus.StatusPending);
                break;
            case OrderHeaderOrderByOptions.Approved:
                linq = linq.Where(x => x.OrderStatus == PaymentStatus.StatusApproved);
                break;
            case OrderHeaderOrderByOptions.Underprocess:
                linq = linq.Where(x => x.OrderStatus == OrderStatus.StatusInProceess);
                break;
            case OrderHeaderOrderByOptions.Shipped:
                linq = linq.Where(x => x.OrderStatus == OrderStatus.StatusShipped);
                break;
        }

        return linq.ToList();
    }
}
