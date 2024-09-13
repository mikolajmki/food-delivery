using System.Security.Claims;
using System.Security.Principal;

namespace Presentation.Areas.Identity;

public static class IdentityClaimHelper
{
    public static string GetIdFromClaim(IIdentity identity)
    {
        var claimsIdentity = (ClaimsIdentity)identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)!;

        return claim.Value;
    }
}