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
    public interface IUserService
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public User GetUserById(int id)
        {
            try
            {
                _logger.Information("Fetching user with ID: {UserId}", id);
                return _userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching user with ID: {UserId}", id);
                throw;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                _logger.Information("Fetching all users");
                return _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching all users");
                throw;
            }
        }

        public void AddUser(User user)
        {
            try
            {
                _logger.Information("Adding new user with nickname: {UserName}", user.Nickname);
                _userRepository.AddUser(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding user with nickname: {UserName}", user.Nickname);
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _logger.Information("Updating user with ID: {UserId}", user.ID);
                _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating user with ID: {UserId}", user.ID);
                throw;
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                _logger.Information("Deleting user with ID: {UserId}", id);
                _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting user with ID: {UserId}", id);
                throw;
            }
        }
    }
}

