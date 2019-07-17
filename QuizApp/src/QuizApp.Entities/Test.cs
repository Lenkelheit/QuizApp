using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(Test))]
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(length: 128)]
        public string Title { get; set; }

        [MaxLength(length: 512)]
        public string Description { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan TimeLimitSeconds { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        public int AuthorId { get; set; }


        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        public ICollection<Url> Urls { get; set; } = new List<Url>();

        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
    }
}
