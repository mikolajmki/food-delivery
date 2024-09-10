using Application.Abstractions.Services;
using System.Security.Claims;
using System.Security.Principal;

namespace Application.Services;

internal class IdentityService : IIdentityService
{
    public string GetIdFromClaim(IIdentity identity)
    {
        var claimsIdentity = (ClaimsIdentity)identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!;

        return claim.Value;
    }
}
