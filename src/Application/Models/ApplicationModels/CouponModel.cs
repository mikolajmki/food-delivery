﻿namespace Application.Models.ApplicationModels;

public class CouponModel : BaseEntityModel
{
    public string Title { get; set; }
    public string Type { get; set; }
    public double Discount { get; set; }
    public double MinimumAmount { get; set; }
    public byte[] CouponPicture { get; set; }
    public bool IsActive { get; set; }
}

public enum CouponType
{
    Percent = 0,
    Currency = 1
}
