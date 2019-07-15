namespace QuizApp.Entities
{
    public static class EntitiesConstraints
    {
        #region User
        public const int USERNAME_MAX_LENGTH = 128;

        public const int EMAIL_MAX_LENGTH = 128;

        public const int PASSWORD_MAX_LENGTH = 256;
        #endregion

        #region Test
        public const int TITLE_MAX_LENGTH = 128;

        public const int DESCRIPTION_MAX_LENGTH = 512;
        #endregion

        #region Url and TestResult
        public const int INTERVIEWEE_NAME_MAX_LENGTH = 128;
        #endregion

        #region TestQuestion
        public const int TEXT_QUESTION_MAX_LENGTH = 512;

        public const int HINT_MAX_LENGTH = 256;
        #endregion

        #region TestQuestionOption
        public const int TEXT_QUESTION_OPTION_MAX_LENGTH = 256;
        #endregion
    }
}
