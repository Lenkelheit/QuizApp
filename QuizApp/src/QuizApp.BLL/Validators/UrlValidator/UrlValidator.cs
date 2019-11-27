using System;
using System.Collections.Generic;
using System.Text;

using FluentValidation;

namespace QuizApp.BLL.Validators.UrlValidator
{
    public class UrlValidator : AbstractValidator<Entities.Url>
    {
        public UrlValidator()
        {
            DateTime checkedUrlTime = DateTime.Now;

            RuleFor(url => url.ValidFromTime)
                .Must(fromTime => checkedUrlTime >= fromTime)
                    .WithMessage("The test will start on {PropertyValue}.");

            RuleFor(url => url.ValidUntilTime)
                .Must(untilTime => checkedUrlTime <= untilTime)
                    .WithMessage("The test ended on {PropertyValue}.");

            RuleFor(url => url.NumberOfRuns)
                .Must(numberOfRuns => !numberOfRuns.HasValue || numberOfRuns > 0)
                    .WithMessage("The number of runs of this test is over.");
        }
    }
}
