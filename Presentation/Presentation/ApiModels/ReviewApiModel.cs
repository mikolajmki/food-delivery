using Application.Models.ApplicationModels;

namespace Presentation.ApiModels;

public class ReviewApiModel : BaseApiModel
{
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int Rating { get; set; }

    public static implicit operator ReviewApiModel(ReviewModel v)
    {
        throw new NotImplementedException();
    }
}
