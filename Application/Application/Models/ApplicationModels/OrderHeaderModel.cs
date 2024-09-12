using Domain.Utility;

namespace Application.Models.ApplicationModels;

public class OrderHeaderModel : BaseEntityModel
{
    public double? SubTotal { get; set; }
    public double OrderTotal { get; set; }
    public DateTime TimeOfPick { get; set; }
    public string ApplicationUserId { get; set; }
    public string? CouponCode { get; set; }
    public double? CouponDis { get; set; }
    public string OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
}
