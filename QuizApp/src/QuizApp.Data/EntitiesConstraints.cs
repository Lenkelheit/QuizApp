namespace QuizApp.Data
{
    public static class EntitiesConstraints
    {
        #region User
        public static readonly int USERNAME_MAX_LENGTH = 128;

        public static readonly int EMAIL_MAX_LENGTH = 128;

        public static readonly int PASSWORD_MAX_LENGTH = 256;
        #endregion

        #region Test
        public static readonly int TITLE_MAX_LENGTH = 128;

        public static readonly int DESCRIPTION_MAX_LENGTH = 512;
        #endregion

        #region Url and TestResult
        public static readonly int INTERVIEWEE_NAME_MAX_LENGTH = 128;
        #endregion

        #region TestQuestion
        public static readonly int TEXT_QUESTION_MAX_LENGTH = 512;

        public static readonly int HINT_MAX_LENGTH = 256;
        #endregion

        #region TestQuestionOption
        public static readonly int TEXT_QUESTION_OPTION_MAX_LENGTH = 256;
        #endregion
    }
}
