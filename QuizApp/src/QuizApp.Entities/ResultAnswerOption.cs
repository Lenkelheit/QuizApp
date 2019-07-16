using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    [Table(nameof(ResultAnswerOption))]
    public class ResultAnswerOption
    {
        [Key]
        public int Id { get; set; }

        public int? OptionId { get; set; }

        public int ResultAnswerId { get; set; }


        [ForeignKey(nameof(OptionId))]
        public TestQuestionOption Option { get; set; }

        [ForeignKey(nameof(ResultAnswerId))]
        public ResultAnswer ResultAnswer { get; set; }
    }
}
