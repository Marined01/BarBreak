namespace BarBreak.Core.Role;

using ErrorOr;

public static class RoleErrors
{
    public static ErrorOr<RoleDto> ValidationFailed =>
        Error.Validation("Role.ValidationFailed", "Role DTO validation failed");

    public static ErrorOr<RoleDto> Duplicate(string roleName) =>
        Error.Conflict("Role.Duplicate", $"Role with Role name( {roleName} ) already exists");

    public static ErrorOr<RoleDto> NotFound(int id) =>
        Error.NotFound("Role.NotFound", $"Role with ID( {id} ) not found");
}