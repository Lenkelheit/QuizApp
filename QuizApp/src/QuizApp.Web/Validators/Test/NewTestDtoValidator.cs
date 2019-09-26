using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.Test;
using QuizApp.Web.Validators.TestQuestion;
using QuizApp.Web.Validators.Url;

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
                    .When(test => test.Description != null)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters in test.");

            RuleFor(test => test.TimeLimitSeconds)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in test.");

            RuleForEach(test => test.TestQuestions).SetValidator(new NewTestQuestionDtoValidator());

            RuleForEach(test => test.Urls).SetValidator(new NewUrlDtoValidator());
        }
    }
}
