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
                    .WithMessage("{PropertyName} is mandatory.")
                .Length(4, 128)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters.");

            RuleFor(test => test.Description)
                .Length(4, 512)
                    .When(test => test.Description != null)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters.");

            RuleFor(test => test.TimeLimitSeconds)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.");
        }
    }
}
