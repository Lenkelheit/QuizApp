using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.TestResult;

namespace QuizApp.Web.Validators.TestResult
{
    public class NewTestResultDtoValidator : AbstractValidator<NewTestResultDto>
    {
        public NewTestResultDtoValidator()
        {
            RuleFor(result => result.IntervieweeName)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.")
                .Length(4, 128)
                    .WithMessage("{PropertyName} must be from {MinLength} to {MaxLength} characters.");

            RuleFor(result => result.PassingStartTime)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.");

            RuleFor(result => result.PassingEndTime)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.")
                .Must((result, endTime) => endTime > result.PassingStartTime)
                    .WithMessage(customer => $"{nameof(customer.PassingEndTime)} must be later than {nameof(customer.PassingStartTime)}.");

            RuleFor(result => result.Score)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory.")
                .InclusiveBetween(0, 100)
                    .WithMessage("{PropertyName} must be between {From} and {To}.");
        }
    }
}
