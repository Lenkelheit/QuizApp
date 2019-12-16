using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.TestQuestionOption;
using QuizApp.Web.Validators.TestQuestionOption;

namespace QuizApp.UnitTests.ValidatorsTest.TestQuestionOption
{
    [TestClass]
    public class NewTestQuestionOptionDtoValidatorTest
    {
        [TestMethod]
        public void Validate_QuestionOptionPropertiesAreDefault_ReturnsErrors()
        {
            var newTestQuestionOptionDto = new NewTestQuestionOptionDto
            {
                Text = default,
                IsRight = default
            };
            var newTestQuestionOptionDtoValidator = new NewTestQuestionOptionDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Text is mandatory in question option."
            };

            var validationResult = newTestQuestionOptionDtoValidator.Validate(newTestQuestionOptionDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_QuestionOptionPropertiesAreNotDefaultButHaveBadLength_ReturnsErrors()
        {
            var newTestQuestionOptionDto = new NewTestQuestionOptionDto
            {
                Text = "t",
                IsRight = true
            };
            var newTestQuestionOptionDtoValidator = new NewTestQuestionOptionDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Text must be from 4 to 256 characters in question option."
            };

            var validationResult = newTestQuestionOptionDtoValidator.Validate(newTestQuestionOptionDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_QuestionOptionPropertiesAreNotDefaultAndHaveGoodLength_NotReturnErrors()
        {
            var newTestQuestionOptionDto = new NewTestQuestionOptionDto
            {
                Text = "Text",
                IsRight = true
            };
            var newTestQuestionOptionDtoValidator = new NewTestQuestionOptionDtoValidator();
            var expectedErrorMessagesCount = 0;

            var validationResult = newTestQuestionOptionDtoValidator.Validate(newTestQuestionOptionDto);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
