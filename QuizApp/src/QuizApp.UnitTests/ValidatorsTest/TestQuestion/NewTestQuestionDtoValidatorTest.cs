using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.TestQuestion;
using QuizApp.Web.Validators.TestQuestion;

namespace QuizApp.UnitTests.ValidatorsTest.TestQuestion
{
    [TestClass]
    public class NewTestQuestionDtoValidatorTest
    {
        [TestMethod]
        public void Validate_QuestionPropertiesAreDefault_IsNotValid()
        {
            var newTestQuestionDto = new NewTestQuestionDto
            {
                Text = default,
                Hint = default,
                TimeLimitSeconds = default
            };
            var newTestQuestionDtoValidator = new NewTestQuestionDtoValidator();

            var validationResult = newTestQuestionDtoValidator.Validate(newTestQuestionDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_QuestionPropertiesAreNotDefaultButHaveBadLength_IsNotValid()
        {
            var newTestQuestionDto = new NewTestQuestionDto
            {
                Text = "t",
                Hint = "h",
                TimeLimitSeconds = TimeSpan.FromSeconds(1)
            };
            var newTestQuestionDtoValidator = new NewTestQuestionDtoValidator();

            var validationResult = newTestQuestionDtoValidator.Validate(newTestQuestionDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_QuestionPropertiesAreNotDefaultAndHaveGoodLength_IsValid()
        {
            var newTestQuestionDto = new NewTestQuestionDto
            {
                Text = "Text",
                Hint = "Hint",
                TimeLimitSeconds = TimeSpan.FromSeconds(1)
            };
            var newTestQuestionDtoValidator = new NewTestQuestionDtoValidator();

            var validationResult = newTestQuestionDtoValidator.Validate(newTestQuestionDto);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
