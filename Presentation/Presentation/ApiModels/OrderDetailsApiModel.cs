using Presentation.ApiModels;

namespace food_delivery.Models;

public class OrderDetailsApiModel : BaseApiModel
{
    public string Name { get; set; }
    public int Count { get; set; }
}
