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
       
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new SignUpResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Email і пароль є обов’язковими полями."
                };
            }
            var response = _userService.SignUpGuest(request);
            return response;
        }
    }
}