using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BarBreak.Core.Entities;
using BarBreak.Core.Repositories;

namespace BarBreak.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; // Database context

        public UserRepository(ApplicationDbContext context)
        {
            _context = context; // Initialize the database context
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id); // Retrieve a user by their ID
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync(); // Retrieve all users
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user); // Add a new user
            await _context.SaveChangesAsync(); // Persist changes to the database
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user); // Update user data
            await _context.SaveChangesAsync(); // Persist changes to the database
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id); // Find user by ID
            if (user != null)
            {
                _context.Users.Remove(user); // Remove the user from the database
                await _context.SaveChangesAsync(); // Persist changes to the database
            }
        }
    }
}
