using food_delivery.Migrations;
using food_delivery.Models;

namespace food_delivery.ViewModels
{
    public class ItemListViewModel
    {
        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Category> CategoriesList { get; set; } = new List<Category>();
        public List<Coupon> Coupons { get; set; } = new List<Coupon>();
        public List<ItemReviewsViewModel> Reviews { get; set; } = new List<ItemReviewsViewModel>();
    }
}
