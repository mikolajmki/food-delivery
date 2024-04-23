using food_delivery.Models;

namespace food_delivery.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
        public List<Review> Reviews { get; set; }
        public Review NewReview { get; set; }
    }
}
