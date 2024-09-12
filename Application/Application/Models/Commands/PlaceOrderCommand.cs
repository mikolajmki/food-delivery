using Application.Models.ApplicationModels;

namespace Application.Models.Commands;
public class PlaceOrderCommand
{
    public List<CartModel> ListOfCart { get; set; }
    public OrderHeaderModel OrderHeader { get; set; }

}
