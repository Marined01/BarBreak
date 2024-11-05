namespace BarBreak.Core.User;

using ErrorOr;
using BarBreak.Core.Entities;
using BarBreak.Core.DTOs;
using BarBreak.Core.Repositories;
using Serilog;

public interface IUserService
{
    Task<ErrorOr<UserDto>> GetUserById(int id);

    Task<IEnumerable<UserEntity>> GetAllUsers();

    Task<ErrorOr<UserDto>> AddUser(UserDto userEntity);

    Task<ErrorOr<UserDto>> UpdateUser(UserDto userEntity);

    Task DeleteUser(int id);

    SignUpResponseDto SignUpGuest(SignUpRequestDto request);
   
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public UserService(IUserRepository userRepository, ILogger logger)
    {
        this._userRepository = userRepository;
        this._logger = logger;
    }
    // Method to sign up a guest
    public SignUpResponseDto SignUpGuest(SignUpRequestDto request)
    {
        // Check if a user with the given email already exists
        if (_userRepository.ExistsByEmail(request.Email))
        {
            return new SignUpResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "A user with this email already exists."
            };
        }

        // Check if a user with the given username already exists
        if (_userRepository.ExistsByUsername(request.Username))
        {
            return new SignUpResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "A user with this username already exists."
            };
        }

        // Create a new user
        var newUser = new UserEntity
        {
            Email = request.Email,
            Password = HashPassword(request.Password), // Ensure you implement password hashing
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username
        };

        // Save the user to the database
        _userRepository.Create(newUser);
        // Consider saving changes if your repository pattern requires it

        return new SignUpResponseDto
        {
            IsSuccess = true,
            ErrorMessage = null
        };
    }

    private string HashPassword(string password)
    {
        // Implement password hashing (e.g., using BCrypt or another algorithm)
        return password; // Replace with actual hashing logic
    }
    // User
    public async Task<ErrorOr<UserDto>> GetUserById(int id)
    {
        try
        {
            this._logger.Information("Fetching user with ID: {UserId}", id);
            var user = await this._userRepository.GetById(id);

            if (user is null)
            {
                return UserErrors.NotFound(id);
            }

            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                Password = user.Password,
                LastName = user.LastName,
                Username = user.Username,
            };
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error fetching user with ID: {UserId}", id);
            throw;
        }
    }

    // Admin
    public async Task<IEnumerable<UserEntity>> GetAllUsers()
    {
        try
        {
            this._logger.Information("Fetching all users");
            return await this._userRepository.GetAll();
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error fetching all users");
            throw;
        }
    }

    // Admin
    public async Task<ErrorOr<UserDto>> AddUser(UserDto userDto)
    {
        try
        {
            if (userDto.Id is not 0)
            {
                return UserErrors.ValidationFailed;
            }

            this._logger.Information("Adding new userEntity with username: {UserName}", userDto.Username);
            var user = new UserEntity()
            {
                Username = userDto.Username,
                Email = userDto.Email,
                Password = userDto.Password,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
            };

            var entity = await this._userRepository.Create(user);
            userDto.Id = entity.Id;
            return userDto;
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error adding userEntity with nickname: {UserName}", userDto.Username);
            throw;
        }
    }

    // Admin (including change a password permission)
    // User
    public async Task<ErrorOr<UserDto>> UpdateUser(UserDto userDto)
    {
        try
        {
            if (userDto.Id <= 0)
            {
                return UserErrors.ValidationFailed;
            }

            this._logger.Information("Updating userEntity with ID: {UserId}", userDto.Id);

            var user = await this._userRepository.GetById(userDto.Id);

            if (user is null)
            {
                return UserErrors.NotFound(userDto.Id);
            }

            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Username = userDto.Username;

            await this._userRepository.Update(user);

            return userDto;
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error updating userEntity with ID: {UserId}", userDto.Id);
            throw;
        }
    }

    // Admin
    public async Task DeleteUser(int id)
    {
        try
        {
            this._logger.Information("Deleting user with ID: {UserId}", id);
            await this._userRepository.Delete(id);
        }
        catch (Exception ex)
        {
            this._logger.Error(ex, "Error deleting user with ID: {UserId}", id);
            throw;
        }
    }
}