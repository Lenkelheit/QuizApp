using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class Test : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan TimeLimitSeconds { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual User Author { get; set; }
        public virtual ICollection<Url> Urls { get; set; } = new List<Url>();
        public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
    }
}
