namespace Application.Models;

public class ReviewModel: BaseEntityModel
{
    public int ItemId { get; set; } = 0;
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int Rating { get; set; }
}
