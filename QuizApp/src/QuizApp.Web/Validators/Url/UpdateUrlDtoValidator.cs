using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.Url;

namespace QuizApp.Web.Validators.Url
{
    public class UpdateUrlDtoValidator : AbstractValidator<UpdateUrlDto>
    {
        public UpdateUrlDtoValidator()
        {
            RuleFor(url => url.Id)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in url.");

            RuleFor(url => url.NumberOfRuns)
                .GreaterThanOrEqualTo(0)
                    .When(url => url.NumberOfRuns != null)
                    .WithMessage("{PropertyName} must be greater than or equal to {ComparisonValue} in url.");

            RuleFor(url => url.ValidFromTime)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in url.");

            RuleFor(url => url.ValidUntilTime)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in url.")
                .Must((url, untilTime) => untilTime > url.ValidFromTime)
                    .WithMessage(url => $"{nameof(url.ValidUntilTime)} must be later than {nameof(url.ValidFromTime)} in url.");

            RuleFor(url => url.IntervieweeName)
                .Length(4, 128)
                    .When(url => !string.IsNullOrEmpty(url.IntervieweeName))
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters in url.");

            RuleFor(url => url.TestId)
                .GreaterThan(0)
                    .When(url => url.TestId != null)
                    .WithMessage("{PropertyName} must be greater than {ComparisonValue} in url.");
        }
    }
}
