using ComplaintPortalEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalEntities.Authentication;

public static class PermissionsProvider
{
    public static List<Permissions> GetAll()
    {
        List<Permissions> permissions = Enum.GetValues(typeof(Permissions)).OfType<Permissions>().ToList();
        permissions.Remove(Permissions.None);
        permissions.Remove(Permissions.Admin);
        permissions.Remove(Permissions.All);

        return permissions;
    }

    public static Permissions GetMaxPermission()
    {
        List<Permissions> permissions = GetAll();
        Permissions permission = Permissions.None;
        foreach (var item in permissions)
        {
            permission |= item;
        }
        return permission;
    }
}