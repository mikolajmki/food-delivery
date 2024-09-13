using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class OrderDetails: BaseEntity
{
    [ForeignKey(nameof(OrderHeaderId))]
    public virtual OrderHeader OrderHeader { get; set; }
    public int OrderHeaderId { get; set; }
    [ForeignKey(nameof(ItemId))]
    public virtual Item Item { get; set; }
    public int? ItemId { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}
