using food_delivery.Models;

namespace food_delivery.ViewModels
{
    public class CartOrderViewModel
    {
        public List<CartApiModel> ListOfCart { get; set; }
        public OrderHeaderApiModel OrderHeader { get; set; }
    }
}
