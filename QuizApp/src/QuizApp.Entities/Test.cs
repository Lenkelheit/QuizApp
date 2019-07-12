using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    public class Test
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "time")]
        public TimeSpan TimeLimitSeconds { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        public User Author { get; set; }
        public ICollection<Url> Urls { get; set; } = new List<Url>();
        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
    }
}
