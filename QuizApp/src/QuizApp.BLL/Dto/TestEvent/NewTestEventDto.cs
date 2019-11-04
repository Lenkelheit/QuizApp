using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.Entities.Enums;

namespace QuizApp.BLL.Dto.TestEvent
{
    public class NewTestEventDto
    {
        public Guid SessionId { get; set; }

        public EventType EventType { get; set; }

        public string Payload { get; set; }
    }
}
