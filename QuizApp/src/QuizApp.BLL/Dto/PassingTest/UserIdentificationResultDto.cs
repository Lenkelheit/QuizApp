using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.PassingTest
{
    public class UserIdentificationResultDto
    {
        public bool IsIdentified { get; set; }

        public bool IsUrlValid { get; set; }

        public ICollection<string> Errors { get; set; } = new List<string>();
    }
}
