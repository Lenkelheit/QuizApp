namespace QuizApp.Entities
{
    public class ResultAnswerOption
    {
        public int Id { get; set; }
        public int? OptionId { get; set; }
        public int ResultAnswerId { get; set; }

        public TestQuestionOption Option { get; set; }
        public ResultAnswer ResultAnswer { get; set; }
    }
}
