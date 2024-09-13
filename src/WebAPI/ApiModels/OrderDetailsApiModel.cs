using Presentation.ViewModels;

namespace Presentation.ApiModels;

public class OrderDetailsApiModel : BaseApiModel
{
    public string Name { get; set; }
    public ItemApiModel Item { get; set; }
    public int ItemId { get; set; }
    public int Count { get; set; }
}
