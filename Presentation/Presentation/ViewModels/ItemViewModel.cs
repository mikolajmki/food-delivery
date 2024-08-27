using Microsoft.AspNetCore.Http;

namespace Presentation.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public int TotalReviewsCount { get; set; }
        public double AverageRating { get; set; }
    }
}
