using Presentation.ApiModels;

namespace Presentation.ViewModels
{
    public class ItemListViewModel
    {
        public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
        public List<CategoryApiModel> Categories { get; set; } = new List<CategoryApiModel>();
        public List<CategoryApiModel> CategoriesList { get; set; } = new List<CategoryApiModel>();
        public List<CouponViewModel> Coupons { get; set; } = new List<CouponViewModel>();
        public List<ItemReviewsViewModel> Reviews { get; set; } = new List<ItemReviewsViewModel>();
    }
}
