using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Validate_QuestionPropertiesAreDefault_ReturnsErrors()
        {
            var newTestQuestionDto = new NewTestQuestionDto
            {
                Text = default,
                Hint = default,
                TimeLimitSeconds = default
            };
            var newTestQuestionDtoValidator = new NewTestQuestionDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Text is mandatory in question.",
                "Time Limit Seconds is mandatory in question."
            };

            var validationResult = newTestQuestionDtoValidator.Validate(newTestQuestionDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_QuestionPropertiesAreNotDefaultButHaveBadLength_ReturnsErrors()
        {
            var newTestQuestionDto = new NewTestQuestionDto
            {
                Text = "t",
                Hint = "h",
                TimeLimitSeconds = new TimeSpan(0, 0, 1)
            };
            var newTestQuestionDtoValidator = new NewTestQuestionDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Text must be from 4 to 512 characters in question.",
                "Hint must be from 4 to 256 characters in question."
            };

            var validationResult = newTestQuestionDtoValidator.Validate(newTestQuestionDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_QuestionPropertiesAreNotDefaultAndHaveGoodLength_NotReturnErrors()
        {
            var newTestQuestionDto = new NewTestQuestionDto
            {
                Text = "Text",
                Hint = "Hint",
                TimeLimitSeconds = new TimeSpan(0, 0, 1)
            };
            var newTestQuestionDtoValidator = new NewTestQuestionDtoValidator();
            var expectedErrorMessagesCount = 0;

            var validationResult = newTestQuestionDtoValidator.Validate(newTestQuestionDto);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
