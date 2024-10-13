using BarBreak.Core.Entities;
using BarBreak.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace BarBreak.Core.Services
{
    public interface IRoleService
    {
        Role GetRoleById(int id);
        IEnumerable<Role> GetAllRoles();
        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(int id);
    }

    internal class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger _logger;

        public RoleService(IRoleRepository roleRepository, ILogger logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public Role GetRoleById(int id)
        {
            try
            {
                _logger.Information("Fetching role with ID: {RoleId}", id);
                var role = _roleRepository.GetRoleById(id);
                return role;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching role with ID: {RoleId}", id);
                throw;
            }
        }

        public IEnumerable<Role> GetAllRoles()
        {
            try
            {
                _logger.Information("Fetching all roles");
                var roles = _roleRepository.GetAllRoles();
                return roles;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching all roles");
                throw;
            }
        }

        public void AddRole(Role role)
        {
            try
            {
                _logger.Information("Adding new role with name: {RoleName}", role.RoleName);
                _roleRepository.AddRole(role);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding role with name: {RoleName}", role.RoleName);
                throw;
            }
        }

        public void UpdateRole(Role role)
        {
            try
            {
                _logger.Information("Updating role with ID: {RoleId}", role.ID);
                _roleRepository.UpdateRole(role);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating role with ID: {RoleId}", role.ID);
                throw;
            }
        }

        public void DeleteRole(int id)
        {
            try
            {
                _logger.Information("Deleting role with ID: {RoleId}", id);
                _roleRepository.DeleteRole(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting role with ID: {RoleId}", id);
                throw;
            }
        }
    }
}
