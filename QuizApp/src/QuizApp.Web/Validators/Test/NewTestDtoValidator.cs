using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.Test;

namespace QuizApp.Web.Validators.Test
{
    public class NewTestDtoValidator : AbstractValidator<NewTestDto>
    {
        public NewTestDtoValidator()
        {
            RuleFor(test => test.Title)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in test.")
                .Length(4, 128)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters in test.");

            RuleFor(test => test.Description)
                .Length(4, 512)
                    .When(test => !string.IsNullOrEmpty(test.Description))
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters in test.");

            RuleFor(test => test.TimeLimitSeconds)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in test.");

            RuleFor(test => test.AuthorId)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in test.");
        }
    }
}
