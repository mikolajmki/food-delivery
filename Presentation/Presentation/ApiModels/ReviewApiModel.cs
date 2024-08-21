namespace Presentation.ApiModels;

public class ReviewApiModel : BaseApiModel
{
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int Rating { get; set; }
}
