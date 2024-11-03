namespace BarBreak.Core.Role;

using BarBreak.Core.Entities;
using Serilog;
using ErrorOr;

public interface IRoleService
{
    Task<ErrorOr<RoleDto>> GetRoleById(int id);

    Task<IEnumerable<RoleDto>> GetAllRoles();

    Task<ErrorOr<RoleDto>> AddRole(RoleDto roleEntity);

    Task<ErrorOr<RoleDto>> UpdateRole(RoleDto roleEntity);

    Task DeleteRole(int id);
}

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger _logger;

    public RoleService(IRoleRepository roleRepository, ILogger logger)
    {
        this._roleRepository = roleRepository;
        this._logger = logger;
    }

    public async Task<ErrorOr<RoleDto>> GetRoleById(int id)
    {
        try
        {
            this._logger.Information("Fetching role with ID: {RoleId}", id);
            var role = await this._roleRepository.GetById(id);
            if (role is null)
            {
                return RoleErrors.NotFound(id);
            }

            return new RoleDto()
            {
                Id = role.Id,
                RoleName = role.RoleName,
            };
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error fetching role with ID: {RoleId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<RoleDto>> GetAllRoles()
    {
        try
        {
            this._logger.Information("Fetching all roles");
            var roles = await this._roleRepository.GetAll();
            return roles.Select(role => new RoleDto()
            {
                Id = role.Id,
                RoleName = role.RoleName,
            });
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error fetching all roles");
            throw;
        }
    }

    public async Task<ErrorOr<RoleDto>> AddRole(RoleDto roleDto)
    {
        try
        {
            if (roleDto.Id is not 0)
            {
                return RoleErrors.ValidationFailed;
            }

            this._logger.Information("Adding new roleEntity with name: {RoleName}", roleDto.RoleName);
            var role = new RoleEntity()
            {
                RoleName = roleDto.RoleName,
            };

            var entity = await this._roleRepository.Create(role);
            roleDto.Id = entity.Id;
            return roleDto;
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error adding roleEntity with name: {RoleName}", roleDto.RoleName);
            throw;
        }
    }

    public async Task<ErrorOr<RoleDto>> UpdateRole(RoleDto roleDto)
    {
        try
        {
            if (roleDto.Id <= 0)
            {
                return RoleErrors.ValidationFailed;
            }

            this._logger.Information("Updating roleEntity with ID: {RoleId}", roleDto.Id);
            var role = await this._roleRepository.GetById(roleDto.Id);

            if (role is null)
            {
                return RoleErrors.NotFound(roleDto.Id);
            }

            role.RoleName = roleDto.RoleName;

            await this._roleRepository.Update(role);

            return roleDto;
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error updating roleEntity with ID: {RoleId}", roleDto.Id);
            throw;
        }
    }

    public async Task DeleteRole(int id)
    {
        try
        {
            this._logger.Information("Deleting role with ID: {RoleId}", id);
            await this._roleRepository.Delete(id);
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error deleting role with ID: {RoleId}", id);
            throw;
        }
    }
}