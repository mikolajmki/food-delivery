using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_delivery.Models
{
    public class Item
    {
        [Key]
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
}
