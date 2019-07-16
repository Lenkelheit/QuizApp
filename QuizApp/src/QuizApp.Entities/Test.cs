using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(Test))]
    public class Test
    {
        public const int TitleMaxLength = 128;

        public const int DescriptionMaxLength = 512;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(length: TitleMaxLength)]
        public string Title { get; set; }

        [MaxLength(length: DescriptionMaxLength)]
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
