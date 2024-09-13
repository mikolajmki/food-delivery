using Domain.Utility;

namespace Presentation.ApiModels;

public class OrderHeaderApiModel : BaseApiModel
{
    public DateTime OrderDate { get; set; }
    public DateTime TimeOfPick { get; set; }
    public DateTime DateOfPick { get; set; }
    public ApplicationUserApiModel ApplicationUser { get; set; }
    public int ApplicationUserId { get; set; }
    public double? SubTotal { get; set; }
    public double OrderTotal { get; set; }
    public string? CouponCode { get; set; }
    public double? CouponDis { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

}
