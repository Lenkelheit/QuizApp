using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using FluentValidation;

using QuizApp.BLL.Dto.Authentication;
using QuizApp.BLL.Settings;

namespace QuizApp.Web.Validators.Authentication
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator(IOptions<UserLogin> userLogin)
        {
            if (userLogin == null) throw new ArgumentNullException(nameof(userLogin));

            RuleFor(user => user)
                .Must(user => user.Email == userLogin.Value.Email && user.Password == userLogin.Value.Password)
                    .WithMessage("Incorrect email or password.");
        }
    }
}
