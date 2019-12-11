using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

using QuizApp.BLL.Dto.Authentication;
using QuizApp.BLL.Settings;

namespace QuizApp.BLL.Validators.AuthenticationValidator
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator(UserLogin userLogin)
        {
            RuleFor(user => user)
                .Must(user => user.Email == userLogin.Email && user.Password == userLogin.Password)
                    .WithMessage("Incorrect email or password.");
        }
    }
}
