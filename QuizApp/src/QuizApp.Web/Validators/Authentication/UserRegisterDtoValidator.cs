using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.Authentication;

namespace QuizApp.Web.Validators.Authentication
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(user => user.Username)
                .NotEmpty()
                    .WithMessage("{PropertyName} is required.")
                .Length(4, 64)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters.");

            RuleFor(user => user.Email)
                .NotEmpty()
                    .WithMessage("{PropertyName} is required.")
                .EmailAddress()
                    .WithMessage("Invalid email format.");

            RuleFor(user => user.Password)
                .NotEmpty()
                    .WithMessage("{PropertyName} is required.")
                .Length(4, 64)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters.");
        }
    }
}
