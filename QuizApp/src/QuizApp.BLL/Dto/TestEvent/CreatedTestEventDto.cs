using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.Entities.Enums;

namespace QuizApp.BLL.Dto.TestEvent
{
    public class CreatedTestEventDto
    {
        public int Id { get; set; }

        public Guid SessionId { get; set; }

        public DateTime StartTime { get; set; }

        public EventType EventType { get; set; }

        public string Payload { get; set; }
    }
}
