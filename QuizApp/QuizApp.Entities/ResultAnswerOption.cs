namespace QuizApp.Entities
{
    public class ResultAnswerOption : EntityBase
    {
        public int? OptionId { get; set; }
        public virtual TestQuestionOption Option { get; set; }
        public virtual ResultAnswer ResultAnswer { get; set; }
    }
}
