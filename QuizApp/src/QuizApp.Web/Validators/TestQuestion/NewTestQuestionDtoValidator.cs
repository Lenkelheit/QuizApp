using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.TestQuestion;

namespace QuizApp.Web.Validators.TestQuestion
{
    public class NewTestQuestionDtoValidator : AbstractValidator<NewTestQuestionDto>
    {
        public NewTestQuestionDtoValidator()
        {
            RuleFor(question => question.Text)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.")
                .Length(4, 512)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters.");

            RuleFor(question => question.Hint)
                .Length(4, 256)
                    .When(test => test.Hint != null)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters.");

            RuleFor(question => question.TimeLimitSeconds)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.");
        }
    }
}
