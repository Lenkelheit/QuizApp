using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.Test;
using QuizApp.Web.Validators.Test;

namespace QuizApp.UnitTests.ValidatorsTest.Test
{
    [TestClass]
    public class NewTestDtoValidatorTest
    {
        [TestMethod]
        public void Validate_TestPropertiesAreDefault_ReturnsErrors()
        {
            var newTestDto = new NewTestDto
            {
                Title = default,
                Description = default,
                TimeLimitSeconds = default,
                AuthorId = default
            };
            var newTestDtoValidator = new NewTestDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Title is mandatory in test.",
                "Time Limit Seconds is mandatory in test.",
                "Author Id is mandatory in test."
            };

            var validationResult = newTestDtoValidator.Validate(newTestDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_TestPropertiesAreNotDefaultButHaveBadLength_ReturnsErrors()
        {
            var newTestDto = new NewTestDto
            {
                Title = "t",
                Description = "des",
                TimeLimitSeconds = new TimeSpan(0, 0, 1),
                AuthorId = 1
            };
            var newTestDtoValidator = new NewTestDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Title must be from 4 to 128 characters in test.",
                "Description must be from 4 to 512 characters in test."
            };

            var validationResult = newTestDtoValidator.Validate(newTestDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_TestPropertiesAreNotDefaultAndHaveGoodLength_NotReturnErrors()
        {
            var newTestDto = new NewTestDto
            {
                Title = "Title",
                Description = "Description",
                TimeLimitSeconds = new TimeSpan(0, 0, 1),
                AuthorId = 1
            };
            var newTestDtoValidator = new NewTestDtoValidator();
            var expectedErrorMessagesCount = 0;

            var validationResult = newTestDtoValidator.Validate(newTestDto);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
