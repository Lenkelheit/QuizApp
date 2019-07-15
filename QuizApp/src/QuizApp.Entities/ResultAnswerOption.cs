using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Entities
{
    public class ResultAnswerOption
    {
        public int Id { get; set; }
        public int? OptionId { get; set; }
        public int ResultAnswerId { get; set; }

        [ForeignKey(nameof(OptionId))]
        public TestQuestionOption Option { get; set; }
        [ForeignKey(nameof(ResultAnswerId))]
        public ResultAnswer ResultAnswer { get; set; }
    }
}
