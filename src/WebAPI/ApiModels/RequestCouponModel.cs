using Microsoft.AspNetCore.Http;

namespace Presentation.ApiModels;

public class RequestCouponModel: BaseApiModel
{
    public string Title { get; set; }
    public string Type { get; set; }
    public double Discount { get; set; }
    public double MinimumAmount { get; set; }
    public byte[] CouponPicture { get; set; }
    public bool IsActive { get; set; }
    public IFormFile file { get; set; }
}
