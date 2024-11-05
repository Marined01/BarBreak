using BarBreak.Core.User;
using BarBreak.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBreak.Application.UseCases
{
    internal class SignUpGuestUseCase
    {
        private readonly IUserService _userService;

        public SignUpGuestUseCase(IUserService userService)
        {
            _userService = userService;
        }

        public SignUpResponseDto Execute(SignUpRequestDto request)
        {
            // Перед початком реєстрації можна виконати додаткову валідацію
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new SignUpResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Email і пароль є обов’язковими полями."
                };
            }

            // Виклик методу реєстрації в UserService
            var response = _userService.SignUpGuest(request);

            // Повернення результату реєстрації
            return response;
        }
    }
}