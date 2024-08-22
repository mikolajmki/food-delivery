namespace Domain.Models;

public class Coupon: BaseEntity
{
    public string Title { get; set; }
    public string Type { get; set; }
    public double Discount{ get; set; }
    public double MinimumAmount { get; set; }
    public byte[] CouponPicture { get; set; }
    public bool IsActive { get; set; }
}

public enum CouponType 
{
    Percent = 0,
    Currency = 1
}
