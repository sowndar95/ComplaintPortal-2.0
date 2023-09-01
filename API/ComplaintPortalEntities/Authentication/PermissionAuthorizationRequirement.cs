using ComplaintPortalEntities;
using Microsoft.AspNetCore.Authorization;

namespace ComplaintPortalEntities.Authentication;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(Permissions permission)
    {
        Permissions = permission;
    }

    public Permissions Permissions { get; }
}
