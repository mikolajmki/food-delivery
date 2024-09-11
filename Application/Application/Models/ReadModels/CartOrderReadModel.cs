using Application.Models.ApplicationModels;

namespace Application.Models.ReadModels;

public class CartOrderReadModel
{
    public List<CartModel> ListOfCart { get; set; }
    public OrderHeaderModel OrderHeader { get; set; }
}
