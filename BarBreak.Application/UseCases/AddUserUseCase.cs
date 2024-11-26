using BarBreak.Core.Course;
using System;

namespace BarBreak.Application.UseCases
{
    public interface IUserRepository
    {
        void AddUser(string username, string email, string role);
    }

    public class AddUserUseCase
    {
        private readonly IUserRepository _repository;

        public AddUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Execute(string username, string email, string role)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new ArgumentException("Invalid email format.");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentException("Role cannot be empty.");
            }

            _repository.AddUser(username, email, role);
        }
    }
}