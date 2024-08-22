using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Review: BaseEntity
{
    public int? ItemId { get; set; }
    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; }
    [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int Rating { get; set; }
}
