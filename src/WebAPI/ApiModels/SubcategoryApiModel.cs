namespace Presentation.ApiModels;

public class SubcategoryApiModel : BaseApiModel
{
    public string Title { get; set; }
    public int CategoryId { get; set; }
    public CategoryApiModel Category { get; set; }
}
