namespace Application.Models.Commands;

public record AddToCartCommand
{
    public string UserId { get; set; }
    public int ItemId { get; set; }
    public int CartCount { get; set; }
}
