using Application.Models.Options;
using System.Security.Principal;

namespace Application.Models.Queries;

public record GetOrderHeadersOfUserQuery
{
    public IIdentity? Identity { get; set; }
    public OrderHeaderOrderByOptions Options { get; set; }

    public GetOrderHeadersOfUserQuery(OrderHeaderOrderByOptions options)
    {
        Options = options;
    }

    public GetOrderHeadersOfUserQuery(IIdentity? identity, OrderHeaderOrderByOptions options)
    {
        Identity = identity;
        Options = options;
    }
}
