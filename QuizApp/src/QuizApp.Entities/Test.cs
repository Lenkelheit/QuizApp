﻿using System;
using System.Collections.Generic;

namespace QuizApp.Entities
{
    public class Test : IEntity<int>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan TimeLimitSeconds { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public int AuthorId { get; set; }

        public bool HasAtLeastOneQuestion => TestQuestions?.Count > 0;


        public User Author { get; set; }

        public ICollection<Url> Urls { get; set; } = new List<Url>();

        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
    }
}
