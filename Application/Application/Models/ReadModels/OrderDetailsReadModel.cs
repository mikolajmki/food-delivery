namespace Application.Models.ReadModels;

public class OrderDetailsReadModel : BaseEntityModel
{
    public OrderHeaderModel OrderHeader { get; set; }
    public IEnumerable<OrderDetailsModel> OrderDetails { get; set; }
    public List<ReviewModel> Reviews { get; set; }
}
