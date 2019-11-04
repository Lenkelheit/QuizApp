using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.TestEvent;

namespace QuizApp.Web.Validators.TestEvent
{
    public class NewTestEventDtoValidator : AbstractValidator<NewTestEventDto>
    {
        public NewTestEventDtoValidator()
        {
            RuleFor(testEvent => testEvent.SessionId)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in test event.");

            RuleFor(testEvent => testEvent.EventType)
                .NotNull()
                    .WithMessage("{PropertyName} is mandatory in test event.")
                .IsInEnum()
                    .WithMessage("{PropertyName} doesn't contain such element in test event.");

            RuleFor(testEvent => testEvent.Payload)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in test event.")
                .MinimumLength(10)
                    .WithMessage("{PropertyName} must be at least {MinLength} characters in test event.");
        }
    }
}
