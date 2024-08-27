namespace Application.Models;

public class CartOrderModel
{
    public List<CartModel> ListOfCart { get; set; }
    public OrderHeaderModel OrderHeader { get; set; }
}