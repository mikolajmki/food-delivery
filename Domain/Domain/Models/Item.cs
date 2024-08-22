using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Item: BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    public int CategoryId { get; set; }
    [ForeignKey(nameof(SubcategoryId))]
    public virtual Subcategory Subcategory { get; set; }
    public int SubcategoryId { get; set; }

}
