using Presentation.ApiModels;
using Presentation.Models;

namespace Presentation.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderHeaderApiModel OrderHeader { get; set; }
        public IEnumerable<OrderDetailsApiModel> OrderDetails { get; set; }
        public List<ReviewApiModel> Reviews { get; set; }
        public ReviewApiModel NewReview { get; set; }
    }
}
