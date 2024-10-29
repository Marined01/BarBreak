// <summary>
// This file contains the IRoleRepository interface which defines methods
// for managing roles.
// </summary>

namespace BarBreak.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using BarBreak.Core.Entities;

    /// <summary>
    /// Interface for managing roles in the system.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Gets the role by its identifier.
        /// </summary>
        /// <param name="id">The role identifier.</param>
        /// <returns>The role.</returns>
        Role GetRoleById(int id);

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>A collection of roles.</returns>
        IEnumerable<Role> GetAllRoles();

        /// <summary>
        /// Adds a new role.
        /// </summary>
        /// <param name="role">The role to add.</param>
        void AddRole(Role role);

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        /// <param name="role">The role to update.</param>
        void UpdateRole(Role role);

        /// <summary>
        /// Deletes a role by its identifier.
        /// </summary>
        /// <param name="id">The role identifier.</param>
        void DeleteRole(int id);
    }
}