using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models;
using Application.Models.ReadModels;
using Domain.Models;
using MapsterMapper;
using System.Linq;

namespace Application.Services;

internal class OrderDetailsService : GenericService<OrderDetailsReadModel, OrderDetails>, IOrderDetailsService
{
    private readonly IOrderDetailsRepository _orderDetailsRepository;
    private readonly IOrderHeaderRepository _orderHeaderRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public OrderDetailsService(
        IGenericRepository<OrderDetails> repository,
        IOrderDetailsRepository orderDetailsRepository,
        IMapper mapper,
        IOrderHeaderRepository orderHeaderRepository,
        IReviewRepository reviewRepository) : base(repository)
    {
        _orderDetailsRepository = orderDetailsRepository;
        _orderHeaderRepository = orderHeaderRepository;
        _mapper = mapper;
        _reviewRepository = reviewRepository;
    }

    public async Task<OrderDetailsReadModel> GetOrderDetails(int orderHeaderId)
    {
        var orderHeader = await _orderHeaderRepository.GetOrderHeaderByIdIncludeUser(orderHeaderId);
        var orderDetails = await _orderDetailsRepository.GetOrderDetailsOfOrderHeader(orderHeaderId);

        //ViewBag.PaymentStatus = new SelectList(new List<string> { "Pending", "Approved", "Rejected", "Delay" });
        //ViewBag.OrderStatus = new SelectList(new List<string> {"Pending", "Refunded", "Approved", "Cancelled", "UnderProcess", "Shipped" });

        var ids = orderDetails
            .Select(x => x.ItemId)
            .Cast<int>()
            .ToList();

        var userId = orderHeader.ApplicationUserId;

        var reviews = _reviewRepository.GetReviewsOfItemsByUser(ids, userId);

        var orderDetailsReadModel = new OrderDetailsReadModel
        {
            OrderHeader = _mapper.Map<OrderHeaderModel>(orderHeader),
            OrderDetails = _mapper.Map<List<OrderDetailsModel>>(orderDetails),
            Reviews = _mapper.Map<List<ReviewModel>>(reviews),
        };

        return orderDetailsReadModel;
    }
}
