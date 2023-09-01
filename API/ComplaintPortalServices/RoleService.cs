using ComplaintPortalEntities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalServices
{
    public sealed class RoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;


        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #region Permissions
        public List<RoleDetails> GetRoles(Guid deptID)
        {
            var roleDtos = _roleManager.Roles
               .Where(w => w.DeparmentId == deptID)
               .Select(r => new RoleDetails(r.Id, r.Name, r.Permissions, r.IsActive))
               .OrderBy(r => r.Name)
               .ToList();

            return roleDtos;
        }

        public List<RoleDetails> GetActiveRoles(Guid deptID)
        {
            var roleDtos = _roleManager.Roles
               .Where(w => w.DeparmentId == deptID || w.IsActive == true)
               .Select(r => new RoleDetails(r.Id, r.Name, r.Permissions, r.IsActive))
               .OrderBy(r => r.Name)
               .ToList();

            return roleDtos;
        }

        public List<RoleDetails> GetRolesWithPermission(Guid deptID, Permissions permissions)
        {
            List<RoleDetails> result = new List<RoleDetails>();
            var roleDtos = GetActiveRoles(deptID);
            foreach (var item in roleDtos)
            {
                if ((item.Permissions & permissions) != 0)
                    result.Add(item);
            }
            return result;
        }

        public async Task<RoleDetails> UpdateConfiguration(RoleDetails updatedRole)
        {
            var role = await _roleManager.FindByIdAsync(updatedRole.Id.ToString());
            var tempRoleDTO = new RoleDetails
            {
                Permissions = role.Permissions
            };

            if (updatedRole.IsGranted.HasValue)
            {
                tempRoleDTO.Set(updatedRole.Permissions, updatedRole.IsGranted.Value);
            }
            role.Permissions = tempRoleDTO.Permissions;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            if (result != null && result.Succeeded == false)
            {
                throw new Exception(result.Errors.Last().Description);
            }
            return updatedRole;
        }
        #endregion

        public async Task<RoleDetails> UpdateRole(RoleDetails roleDto, Guid deptID)
        {
            var role = await _roleManager.FindByIdAsync(roleDto.Id.ToString());
            IdentityResult result = null;

            if (role != null)
            {
                role.Name = roleDto.Name;
                role.IsActive = roleDto.IsActive;
                result = await _roleManager.UpdateAsync(role);
            }
            else
            {
                var newrole = new ApplicationRole { Name = roleDto.Name, DeparmentId = deptID, IsActive = roleDto.IsActive };
                result = await _roleManager.CreateAsync(newrole);
            }
            if (result != null && result.Succeeded == false)
            {
                //var validationFailure = new List<ValidationFailure>() { new ValidationFailure() { PropertyName = "Name", ErrorMessage = result?.Errors?.Last()?.Description ?? string.Empty } };
                throw new Exception();
            }
            return roleDto;
        }

        public async Task<RoleDetails> DeleteRole(RoleDetails roleDto)
        {
            var role = await _roleManager.FindByIdAsync(roleDto.Id.ToString());
            if (role == null)
                throw new Exception($"Cannot Delete Role {roleDto.Name}.Role does not Exist");

            await _roleManager.DeleteAsync(role);

            return roleDto;
        }
    }
}
