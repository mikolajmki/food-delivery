namespace Domain.Utility;

public static class PaymentSettings
{
    public const string Stripe = "Stripe";
    public static StripeOptions StripeOptions { get; set; } = new StripeOptions();
}

public class StripeOptions
{
    public string PublishableKey;
    public string SecretKey;
}
