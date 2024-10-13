using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BarBreak.Core.Entities;
using BarBreak.Core.Repositories;

namespace BarBreak.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; // Контекст бази даних

        public UserRepository(ApplicationDbContext context)
        {
            _context = context; // Ініціалізуємо контекст
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id); // Повертає користувача за ID
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync(); // Повертає всіх користувачів
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user); // Додає нового користувача
            await _context.SaveChangesAsync(); // Зберігає зміни
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user); // Оновлює дані користувача
            await _context.SaveChangesAsync(); // Зберігає зміни
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id); // Знаходимо користувача за ID
            if (user != null)
            {
                _context.Users.Remove(user); // Видаляє користувача
                await _context.SaveChangesAsync(); // Зберігає зміни
            }
        }
    }
}
