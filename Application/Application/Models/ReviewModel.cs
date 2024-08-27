namespace Application.Models;

public class ReviewModel: BaseEntityModel
{
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int Rating { get; set; }
}
