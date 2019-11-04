using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.PassingTest;

namespace QuizApp.Web.Validators.PassingTest
{
    public class UserUrlDtoValidator : AbstractValidator<UserUrlDto>
    {
        public UserUrlDtoValidator()
        {
            RuleFor(userUrl => userUrl.UrlId)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in user url.");

            RuleFor(userUrl => userUrl.SessionId)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in user url.");
        }
    }
}
