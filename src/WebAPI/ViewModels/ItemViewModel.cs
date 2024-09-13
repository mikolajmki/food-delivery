namespace Presentation.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public IFormFile ImageUrl { get; set; } = new FormFile(FileStream.Null, 1, 1, "", "");
        public string Image { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public int TotalReviewsCount { get; set; }
        public double AverageRating { get; set; }
    }
}
