namespace Presentation.ApiModels;

public class CouponApiModel: BaseApiModel
{
    public string Title { get; set; }
    public string Type { get; set; }
    public double Discount { get; set; }
    public double MinimumAmount { get; set; }
    public byte[] CouponPicture { get; set; }
    public bool IsActive { get; set; }
}
