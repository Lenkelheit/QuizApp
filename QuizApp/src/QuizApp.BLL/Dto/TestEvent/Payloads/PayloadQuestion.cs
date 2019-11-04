using System;
using System.Collections.Generic;
using System.Text;

namespace QuizApp.BLL.Dto.TestEvent.Payloads
{
    public class PayloadQuestion
    {
        public int QuestionId { get; set; }


        public ICollection<int> SelectedOptionsId { get; set; } = new List<int>();
    }
}
