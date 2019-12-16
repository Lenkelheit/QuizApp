using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.TestResult;
using QuizApp.Web.Validators.TestResult;

namespace QuizApp.UnitTests.ValidatorsTest.TestResult
{
    [TestClass]
    public class NewTestResultDtoValidatorTest
    {
        [TestMethod]
        public void Validate_TestResultPropertiesAreDefault_ReturnsErrors()
        {
            var newTestResultDto = new NewTestResultDto
            {
                IntervieweeName = default,
                PassingStartTime = default,
                PassingEndTime = default,
                Score = default
            };
            var newTestResultDtoValidator = new NewTestResultDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Interviewee Name is mandatory in test result.",
                "Passing Start Time is mandatory in test result.",
                "Passing End Time is mandatory in test result.",
                "PassingEndTime must be later than PassingStartTime in test result."
            };

            var validationResult = newTestResultDtoValidator.Validate(newTestResultDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_TestResultPropertiesHaveDifferentErrors_ReturnsErrors()
        {
            var newTestResultDto = new NewTestResultDto
            {
                IntervieweeName = "i",
                PassingStartTime = new DateTime(1, 1, 3),
                PassingEndTime = new DateTime(1, 1, 2),
                Score = 120
            };
            var newTestResultDtoValidator = new NewTestResultDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Interviewee Name must be from 4 to 128 characters in test result.",
                "PassingEndTime must be later than PassingStartTime in test result.",
                "Score must be between 0 and 100 in test result."
            };

            var validationResult = newTestResultDtoValidator.Validate(newTestResultDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_TestResultPropertiesAreWithoutErrors_NotReturnErrors()
        {
            var newTestResultDto = new NewTestResultDto
            {
                IntervieweeName = "IntervieweeName",
                PassingStartTime = new DateTime(1, 1, 2),
                PassingEndTime = new DateTime(1, 1, 3),
                Score = 100
            };
            var newTestResultDtoValidator = new NewTestResultDtoValidator();
            var expectedErrorMessagesCount = 0;

            var validationResult = newTestResultDtoValidator.Validate(newTestResultDto);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
