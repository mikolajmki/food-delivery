using Presentation.ApiModels;

namespace Presentation.ViewModels
{
    public class CartOrderViewModel
    {
        public List<CartApiModel> ListOfCart { get; set; }
        public OrderHeaderApiModel OrderHeader { get; set; }
    }
}
