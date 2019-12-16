using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Validators.UrlValidator;

namespace QuizApp.UnitTests.ValidatorsTest.UrlValidator
{
    [TestClass]
    public class UrlIntervieweeNameValidatorTest
    {
        [TestMethod]
        public void Validate_UrlPropertiesAreDefault_NotReturnErrors()
        {
            var url = new Entities.Url
            {
                IntervieweeName = default
            };
            var actualIntervieweeName = "IntervieweeName";
            var urlIntervieweeNameValidator = new UrlIntervieweeNameValidator(actualIntervieweeName);
            var expectedErrorMessagesCount = 0;

            var validationResult = urlIntervieweeNameValidator.Validate(url);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }

        [TestMethod]
        public void Validate_IntervieweeNamesAreDifferent_ReturnsErrors()
        {
            var url = new Entities.Url
            {
                IntervieweeName = "First IntervieweeName"
            };
            var actualIntervieweeName = "Second IntervieweeName";
            var urlIntervieweeNameValidator = new UrlIntervieweeNameValidator(actualIntervieweeName);
            var expectedErrorMessages = new string[]
            {
                "Interviewee name is wrong."
            };

            var validationResult = urlIntervieweeNameValidator.Validate(url);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_IntervieweeNamesAreTheSame_NotReturnErrors()
        {
            var url = new Entities.Url
            {
                IntervieweeName = "IntervieweeName"
            };
            var actualIntervieweeName = "IntervieweeName";
            var urlIntervieweeNameValidator = new UrlIntervieweeNameValidator(actualIntervieweeName);
            var expectedErrorMessagesCount = 0;

            var validationResult = urlIntervieweeNameValidator.Validate(url);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
