namespace Application.Models.ApplicationModels;

public class ItemReviewsModel
{
    public int ItemId { get; set; }
    public int TotalReviewsCount { get; set; }
    public double AverageRating { get; set; }
}