using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_delivery.Models;

public class Subcategory: BaseEntity
{
    [Required]
    public string Title { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category? Category { get; set; }
    public int? CategoryId { get; set; }
}
