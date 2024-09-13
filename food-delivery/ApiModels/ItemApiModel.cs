namespace Presentation.ApiModels;

public class ItemApiModel : BaseApiModel
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public CategoryApiModel Category { get; set; }
    public SubcategoryApiModel Subcategory { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }

}
