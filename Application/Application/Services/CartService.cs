using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Application.Models.Commands;
using Application.Models.ReadModels;
using Domain.Models;
using MapsterMapper;

namespace Application.Services;

internal class CartService : GenericService<CartModel, Cart>, ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IApplicationUserRepository _userRepository;
    private readonly IOrderHeaderRepository _orderHeaderRepository;
    private readonly IOrderDetailsRepository _orderDetailsRepository;
    private readonly IMapper _mapper;

    public CartService(
        IGenericRepository<Cart> repository,
        ICartRepository cartRepository,
        IMapper mapper,
        IApplicationUserRepository userRepository,
        IOrderHeaderRepository orderHeaderRepository,
        IOrderDetailsRepository orderDetailsRepository
        
        ) : base(repository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _orderHeaderRepository = orderHeaderRepository;
        _orderDetailsRepository = orderDetailsRepository;
    }

    public Task<CartModel> GetCart()
    {
        throw new NotImplementedException();
    }
    public async Task<CartOrderReadModel> GetCartOfUserIncludeItemsAndOrderTotal(string userId)
    {

        var list = await _cartRepository.GetCartsOfUserIncludeItems(userId);

        var carts = _mapper.Map<List<CartModel>>(list);

        var cartOrderReadModel = new CartOrderReadModel
        {
            ListOfCart = carts,
            OrderHeader = new OrderHeaderModel { OrderTotal = CalculateOrderTotal(list) }
        };

        return cartOrderReadModel;
    }

    public async Task<CartOrderReadModel> GetSummary(string userId)
    {

        var list = await _cartRepository.GetCartsOfUserIncludeItems(userId);
        var carts = _mapper.Map<List<CartModel>>(list);

        var user = await _userRepository.GetById(int.Parse(userId));

        var orderHeader = new OrderHeaderModel
        {
            ApplicationUserId = userId,
            Name = user.Name,
            Phone = user.PhoneNumber,
            OrderTotal = CalculateOrderTotal(list),
            TimeOfPick = DateTime.Now
        };

        return new CartOrderReadModel
        {
            ListOfCart = carts,
            OrderHeader = orderHeader
        };
    }

    public async Task<bool> PlaceOrder(PlaceOrderCommand placeOrderWriteModel)
    {
        var orderHeader = _mapper.Map<OrderHeader>(placeOrderWriteModel.OrderHeader);
        await _orderHeaderRepository.Create(orderHeader);

        var carts = await _cartRepository.GetCartsOfUserIncludeItems(orderHeader.ApplicationUserId);

        foreach (var cart in carts)
        {
            OrderDetails orderDetails = new OrderDetails
            {
                OrderHeaderId = orderHeader.Id,
                ItemId = cart.Item.Id,
                Name = orderHeader.Name,
                Count = cart.Count
            };
            await _orderDetailsRepository.Create(orderDetails);
        }

        return true;
    }

    private static double CalculateOrderTotal(List<Cart> list)
    {
        var orderTotal = 0.0;

        list.ForEach(x =>
        {
            orderTotal += x.Item.Price * x.Count;
        });

        return orderTotal;
    }

    public async Task<bool> AddToCart(int id)
    {
        await _cartRepository.AddToCart(id);

        return true;
    }

    public async Task<bool> RemoveFromCart(int id)
    {
        await _cartRepository.RemoveFromCart(id);

        return true;
    }

    public Task<bool> DeleteCartOfUser(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetUserCartsCount(string userId)
    {
        var count = await _cartRepository.GetUserCartsCount(userId);

        return count;
    }

    public async Task<bool> AddItemToCart(AddToCartCommand command)
    {
        var cart = await _cartRepository.GetCartbyUserIdAndItemId(command.UserId, command.ItemId);

        if (cart == null)
        {
            var newCart = new Cart
            {
                ItemId = command.ItemId,
                ApplicationUserId = command.UserId,
                Count = command.CartCount,
            };

            await _cartRepository.Create(newCart);
        }
        else
        {
            cart.Count += command.CartCount;
            await _cartRepository.Update(cart.Id, cart);
        }

        return true;
    }
}
