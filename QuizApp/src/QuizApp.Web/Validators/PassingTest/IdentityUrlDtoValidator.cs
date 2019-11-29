using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.UrlValidator;

namespace QuizApp.Web.Validators.PassingTest
{
    public class IdentityUrlDtoValidator : AbstractValidator<IdentityUrlDto>
    {
        public IdentityUrlDtoValidator()
        {
            RuleFor(url => url.Id)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in url.");

            RuleFor(url => url.IntervieweeName)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in url.")
                .Length(4, 128)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters in url.");
        }
    }
}
