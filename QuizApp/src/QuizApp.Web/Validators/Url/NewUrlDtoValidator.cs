using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.Url;

namespace QuizApp.Web.Validators.Url
{
    public class NewUrlDtoValidator : AbstractValidator<NewUrlDto>
    {
        public NewUrlDtoValidator()
        {
            RuleFor(url => url.ValidFromTime)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.");

            RuleFor(url => url.ValidUntilTime)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.")
                .Must((url, untilTime) => untilTime > url.ValidFromTime)
                    .WithMessage(url => $"{nameof(url.ValidUntilTime)} must be later than {nameof(url.ValidFromTime)}.");

            RuleFor(url => url.IntervieweeName)
                .Length(4, 128)
                    .When(url => url.IntervieweeName != null)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters.");
        }
    }
}
