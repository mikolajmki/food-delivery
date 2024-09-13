namespace Application.Models.ApplicationModels;

public class ItemModel : BaseEntityModel
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int SubcategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
    public int TotalReviewsCount { get; set; }
    public double AverageRating { get; set; }

}
