using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.Web.Validators.TestQuestionOption;

namespace QuizApp.Web.Validators.TestQuestion
{
    public class NewTestQuestionDtoValidator : AbstractValidator<NewTestQuestionDto>
    {
        public NewTestQuestionDtoValidator()
        {
            RuleFor(question => question.Text)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in question.")
                .Length(4, 512)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters in question.");

            RuleFor(question => question.Hint)
                .Length(4, 256)
                    .When(question => !string.IsNullOrEmpty(question.Hint))
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters in question.");

            RuleFor(question => question.TimeLimitSeconds)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in question.");

            RuleForEach(question => question.TestQuestionOptions).SetValidator(new NewTestQuestionOptionDtoValidator());
        }
    }
}
