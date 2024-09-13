using Domain.Models;

namespace Presentation.ApiModels;

public class CartApiModel: BaseApiModel
{
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public string ApplicationUserId { get; set; }
    public int Count { get; set; }
}
