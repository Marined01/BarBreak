using System.Collections.Generic;
using System.Threading.Tasks;
using BarBreak.Core.Entities;

namespace BarBreak.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id); // Отримати користувача за ID
        Task<IEnumerable<User>> GetAllUsersAsync(); // Отримати всіх користувачів
        Task AddUserAsync(User user); // Додати нового користувача
        Task UpdateUserAsync(User user); // Оновити дані користувача
        Task DeleteUserAsync(int id); // Видалити користувача за ID
    }
}
