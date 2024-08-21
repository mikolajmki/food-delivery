using food_delivery.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_delivery.ViewModels
{
    public class ItemDetailsViewModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public ItemApiModel Item { get; set; }
        public string ApplicationUserId { get; set; }
        public List<ReviewApiModel> Reviews { get; set; }
        public int Count { get; set; }
    }
}
