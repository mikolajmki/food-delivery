namespace Application.Models;

public class ItemModel: BaseEntityModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }

}
