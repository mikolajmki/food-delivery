using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_delivery.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
