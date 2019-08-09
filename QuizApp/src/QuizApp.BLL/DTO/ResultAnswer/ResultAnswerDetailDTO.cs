using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.BLL.DTO.ResultAnswerOption;

namespace QuizApp.BLL.DTO.ResultAnswer
{
    public class ResultAnswerDetailDTO
    {
        public int Id { get; set; }

        public TimeSpan TimeTakenSeconds { get; set; }


        public ICollection<ResultAnswerOptionDetailDTO> ResultAnswerOptions { get; set; }
    }
}
