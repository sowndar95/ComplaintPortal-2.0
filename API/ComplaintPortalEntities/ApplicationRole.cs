using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole
    {
        public Guid? DeparmentId { get; set; }
        public Permissions Permissions { get; set; }
        public bool IsActive { get; set; }
    }

    public class DepartmentRoleValidator : RoleValidator<ApplicationRole>
    {
        public override async Task<IdentityResult> ValidateAsync(RoleManager<ApplicationRole> roleManager, ApplicationRole role)
        {
            var roleName = role.Name.Trim();
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNameEmpty",
                    Description = "Invalid Role Name"
                });
            }
            else
            {
                var existingRole = roleManager.Roles.FirstOrDefault(x => x.DeparmentId == role.DeparmentId && x.NormalizedName == roleName.ToUpper() && x.Id != role.Id);
                if (existingRole != null && !string.Equals(roleManager.GetRoleIdAsync(existingRole), roleManager.GetRoleIdAsync(role)))
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "DuplicateRole",
                        Description = "Role already exist in Department"
                    });
                }
            }
            return IdentityResult.Success;
        }
    }
}
