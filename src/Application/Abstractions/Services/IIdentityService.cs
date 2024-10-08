﻿using System.Security.Claims;
using System.Security.Principal;

namespace Application.Abstractions.Services;

public interface IIdentityService
{
    public string GetIdFromClaim(IIdentity identity);
}
