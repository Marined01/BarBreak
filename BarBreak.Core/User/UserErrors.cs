namespace BarBreak.Core.User;

using ErrorOr;

public static class UserErrors
{
    public static ErrorOr<UserDto> ValidationFailed =>
        Error.Validation("User.ValidationFailed", "User DTO validation failed");

    public static ErrorOr<UserDto> Duplicate(string email) =>
        Error.Conflict("User.Duplicate", $"User with email( {email} ) already exists");

    public static ErrorOr<UserDto> NotFound(int id) =>
        Error.NotFound("User.NotFound", $"User with ID( {id} ) not found");
}