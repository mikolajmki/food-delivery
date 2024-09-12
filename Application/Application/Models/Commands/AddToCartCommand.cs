using System.Security.Principal;

namespace Application.Models.Commands;

public record AddToCartCommand
{
    public IIdentity Identity { get; set; }
    public int ItemId { get; set; }
    public int CartCount { get; set; }
}
