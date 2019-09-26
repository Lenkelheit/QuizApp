using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.TestQuestionOption;

namespace QuizApp.Web.Validators.TestQuestionOption
{
    public class NewTestQuestionOptionDtoValidator : AbstractValidator<NewTestQuestionOptionDto>
    {
        public NewTestQuestionOptionDtoValidator()
        {
            RuleFor(option => option.Text)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in question option.")
                .Length(4, 256)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters in question option.");

            RuleFor(option => option.IsRight)
                .NotNull()
                    .WithMessage("{PropertyName} is mandatory in question option.");
        }
    }
}
