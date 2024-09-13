using Presentation.ApiModels;

namespace Presentation.ViewModels
{
    public class ItemDetailsViewModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public ItemApiModel Item { get; set; }
        public string ApplicationUserId { get; set; }
        public List<ReviewApiModel> Reviews { get; set; }
        public int Count { get; set; }
    }
}
