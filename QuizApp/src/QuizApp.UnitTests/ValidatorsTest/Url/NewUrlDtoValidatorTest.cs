using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.Url;
using QuizApp.Web.Validators.Url;

namespace QuizApp.UnitTests.ValidatorsTest.Url
{
    [TestClass]
    public class NewUrlDtoValidatorTest
    {
        [TestMethod]
        public void Validate_UrlPropertiesAreDefault_ReturnsErrors()
        {
            var newUrlDto = new NewUrlDto
            {
                NumberOfRuns = default,
                ValidFromTime = default,
                ValidUntilTime = default,
                IntervieweeName = default,
                TestId = default
            };
            var newUrlDtoValidator = new NewUrlDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Valid From Time is mandatory in url.",
                "Valid Until Time is mandatory in url.",
                "ValidUntilTime must be later than ValidFromTime in url."
            };

            var validationResult = newUrlDtoValidator.Validate(newUrlDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_UrlPropertiesHaveDifferentErrors_ReturnsErrors()
        {
            var newUrlDto = new NewUrlDto
            {
                NumberOfRuns = -1,
                ValidFromTime = new DateTime(1, 1, 3),
                ValidUntilTime = new DateTime(1, 1, 2),
                IntervieweeName = "i",
                TestId = 0
            };
            var newUrlDtoValidator = new NewUrlDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Number Of Runs must be greater than or equal to 0 in url.",
                "ValidUntilTime must be later than ValidFromTime in url.",
                "Interviewee Name must be from 4 to 128 characters in url.",
                "Test Id must be greater than 0 in url."
            };

            var validationResult = newUrlDtoValidator.Validate(newUrlDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_UrlPropertiesAreWithoutErrors_NotReturnErrors()
        {
            var newUrlDto = new NewUrlDto
            {
                NumberOfRuns = 0,
                ValidFromTime = new DateTime(1, 1, 2),
                ValidUntilTime = new DateTime(1, 1, 3),
                IntervieweeName = "IntervieweeName",
                TestId = 1
            };
            var newUrlDtoValidator = new NewUrlDtoValidator();
            var expectedErrorMessagesCount = 0;

            var validationResult = newUrlDtoValidator.Validate(newUrlDto);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
