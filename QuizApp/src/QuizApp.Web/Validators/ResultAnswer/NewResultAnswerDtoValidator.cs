using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.ResultAnswer;

namespace QuizApp.Web.Validators.ResultAnswer
{
    public class NewResultAnswerDtoValidator : AbstractValidator<NewResultAnswerDto>
    {
        public NewResultAnswerDtoValidator()
        {
            RuleFor(answer => answer.TimeTakenSeconds)
                .NotEmpty()
                    .WithMessage("{PropertyName} is mandatory in result answer.");
        }
    }
}
