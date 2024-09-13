namespace Application.Models.ApplicationModels;

public class ItemListReadModel
{
    public List<ItemModel> Items { get; set; } = new();
    public List<CategoryModel> Categories { get; set; } = new();
    public List<CategoryModel> CategoriesList { get; set; } = new();
    public List<CouponModel> Coupons { get; set; } = new();
    public List<ItemReviewsModel> Reviews { get; set; } = new();
}