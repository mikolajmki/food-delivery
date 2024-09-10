namespace Application.Models;

public class OrderDetailsModel
{
    public int OrderHeaderId { get; set; }
    public int? ItemId { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}
