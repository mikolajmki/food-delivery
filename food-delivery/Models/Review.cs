using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_delivery.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
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
}
