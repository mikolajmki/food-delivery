namespace Application.Models.ApplicationModels;

public class ItemListModel
{
    public List<ItemModel> Items { get; set; } = new List<ItemModel>();
    public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    public List<CategoryModel> CategoriesList { get; set; } = new List<CategoryModel>();
    public List<CouponModel> Coupons { get; set; } = new List<CouponModel>();
    public List<ItemReviewsModel> Reviews { get; set; } = new List<ItemReviewsModel>();
}