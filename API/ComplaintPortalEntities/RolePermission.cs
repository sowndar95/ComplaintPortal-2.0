using ComplaintPortalEntities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    public sealed class RolePermission
    {
        internal RolePermission() { }
        public RolePermission(List<RoleDetails> roles)
        {
            Roles = roles;

            foreach (var permission in PermissionsProvider.GetAll())
            {
                AvailablePermissions.Add(permission);
                PermissionNames.Add(permission.ToString());
            }
        }

        public List<RoleDetails> Roles { get; set; } = new();

        public List<Permissions> AvailablePermissions { get; set; } = new();
        public List<string> PermissionNames { get; set; } = new();
    }



    public sealed class RoleDetails
    {
        public RoleDetails()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Permissions = Permissions.None;
        }

        public RoleDetails(Guid id, string name, Permissions permissions, bool isActive)
        {
            Id = id;
            Name = name;
            Permissions = permissions;
            IsActive = isActive;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Permissions Permissions { get; set; }

        public bool IsActive { get; set; }

        //TODO
        public bool? IsGranted { get; set; } = null;


        public bool Has(Permissions permission)
        {
            return Permissions.HasFlag(permission); ;
        }

        public void Set(Permissions permission, bool granted)
        {
            if (granted)
            {
                Grant(permission);
            }
            else
            {
                Revoke(permission);
            }
        }

        public void Grant(Permissions permission)
        {
            Permissions |= permission;
        }

        public void Revoke(Permissions permission)
        {
            Permissions ^= permission;
        }
    }
}
