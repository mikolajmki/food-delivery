using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Models.ApplicationModels;
using Application.Models.ReadModels;
using Domain.Models;
using MapsterMapper;
using System.Security.Principal;

namespace Application.Services;

internal class CartService : GenericService<CartModel, Cart>, ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IApplicationUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public CartService(
        IGenericRepository<Cart> repository,
        ICartRepository cartRepository,
        IMapper mapper,
        IIdentityService identityService,
        IApplicationUserRepository userRepository

        ) : base(repository)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _identityService = identityService;
        _userRepository = userRepository;
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

    private static double CalculateOrderTotal(List<Cart> list)
    {
        var orderTotal = 0.0;

        list.ForEach(x =>
        {
            orderTotal += x.Item.Price * x.Count;
        });

        return orderTotal;
    }
}
