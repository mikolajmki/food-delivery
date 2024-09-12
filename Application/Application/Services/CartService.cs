using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Application.Models.Commands;
using Application.Models.ReadModels;
using Domain.Models;
using MapsterMapper;
using System.Security.Principal;

namespace Application.Services;

internal class CartService : GenericService<CartModel, Cart>, ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IApplicationUserRepository _userRepository;
    private readonly IIdentityService _identityService;
    private readonly IOrderHeaderRepository _orderHeaderRepository;
    private readonly IOrderDetailsRepository _orderDetailsRepository;
    private readonly IMapper _mapper;

    public CartService(
        IGenericRepository<Cart> repository,
        ICartRepository cartRepository,
        IMapper mapper,
        IIdentityService identityService,
        IApplicationUserRepository userRepository,
        IOrderHeaderRepository orderHeaderRepository,
        IOrderDetailsRepository orderDetailsRepository
        
        ) : base(repository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _identityService = identityService;
        _userRepository = userRepository;
        _orderHeaderRepository = orderHeaderRepository;
        _orderDetailsRepository = orderDetailsRepository;
    }

    public Task<CartModel> GetCart()
    {
        throw new NotImplementedException();
    }
    public async Task<CartOrderReadModel> GetCartOfUserIncludeItemsAndOrderTotal(IIdentity identity)
    {
        var id = _identityService.GetIdFromClaim(identity);

        var list = await _cartRepository.GetCartsOfUserIncludeItems(id);
        


        var carts = _mapper.Map<List<CartModel>>(list);

        var cartOrderReadModel = new CartOrderReadModel
        {
            ListOfCart = carts,
            OrderHeader = new OrderHeaderModel { OrderTotal = CalculateOrderTotal(list) }
        };

        return cartOrderReadModel;
    }

    public async Task<CartOrderReadModel> GetSummary(IIdentity identity)
    {
        var id = _identityService.GetIdFromClaim(identity);

        var list = await _cartRepository.GetCartsOfUserIncludeItems(id);
        var carts = _mapper.Map<List<CartModel>>(list);

        var user = await _userRepository.GetById(int.Parse(id));

        var orderHeader = new OrderHeaderModel
        {
            ApplicationUserId = id,
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

    public Task<bool> DeleteCartOfUser(IIdentity identity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetUserCartsCount(IIdentity identity)
    {
        var id = _identityService.GetIdFromClaim(identity);
        var count = await _cartRepository.GetUserCartsCount(id);

        return count;
    }

    public async Task<bool> AddItemToCart(AddToCartCommand command)
    {
        var id = _identityService.GetIdFromClaim(command.Identity);
        var cart = await _cartRepository.GetCartbyUserIdAndItemId(id, command.ItemId);

        if (cart == null)
        {
            var newCart = new Cart
            {
                ItemId = command.ItemId,
                ApplicationUserId = id,
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
