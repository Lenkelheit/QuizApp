using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;

namespace QuizApp.BLL.Validators.PassingTest
{
    public class UrlIntervieweeNameValidator : AbstractValidator<Entities.Url>
    {
        public UrlIntervieweeNameValidator(string actualIntervieweeName)
        {
            RuleFor(url => url.IntervieweeName)
                .Must(expectedIntervieweeName => string.IsNullOrEmpty(expectedIntervieweeName) || expectedIntervieweeName == actualIntervieweeName)
                    .WithMessage("Interviewee name is wrong.");
        }
    }
}
