using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using QuizApp.BLL.Dto.ResultAnswerOption;

namespace QuizApp.Web.Validators.ResultAnswerOption
{
    public class NewResultAnswerOptionDtoValidator : AbstractValidator<NewResultAnswerOptionDto>
    {
        public NewResultAnswerOptionDtoValidator()
        {
        }
    }
}
