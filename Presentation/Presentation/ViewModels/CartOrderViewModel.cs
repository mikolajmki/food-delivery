using food_delivery.Models;

namespace food_delivery.ViewModels
{
    public class CartOrderViewModel
    {
        public List<Cart> ListOfCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
