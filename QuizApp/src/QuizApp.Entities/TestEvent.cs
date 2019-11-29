using System;
using System.Collections.Generic;
using System.Text;

using QuizApp.Entities.Enums;

namespace QuizApp.Entities
{
    public class TestEvent : IEntity<int>
    {
        public int Id { get; set; }

        public Guid SessionId { get; set; }

        public DateTime StartTime { get; set; }

        public EventType EventType { get; set; }

        public string Payload { get; set; }
    }
}
