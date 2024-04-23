using food_delivery.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_delivery.ViewModels
{
    public class ItemDetailsViewModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public string ApplicationUserId { get; set; }
        public List<Review> Reviews { get; set; }
        public int Count { get; set; }
    }
}
