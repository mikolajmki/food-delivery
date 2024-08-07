using food_delivery.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace food_delivery.ViewModels
{
    public class SubcategoryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }
    }
}
