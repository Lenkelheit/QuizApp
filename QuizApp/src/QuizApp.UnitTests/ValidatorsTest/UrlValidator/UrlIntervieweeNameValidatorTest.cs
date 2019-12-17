using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Validators.UrlValidator;

namespace QuizApp.UnitTests.ValidatorsTest.UrlValidator
{
    [TestClass]
    public class UrlIntervieweeNameValidatorTest
    {
        [TestMethod]
        public void Validate_UrlPropertiesAreDefault_IsValid()
        {
            var url = new Entities.Url
            {
                IntervieweeName = default
            };
            var actualIntervieweeName = "IntervieweeName";
            var urlIntervieweeNameValidator = new UrlIntervieweeNameValidator(actualIntervieweeName);

            var validationResult = urlIntervieweeNameValidator.Validate(url);

            Assert.IsTrue(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_IntervieweeNamesAreDifferent_IsNotValid()
        {
            var url = new Entities.Url
            {
                IntervieweeName = "First IntervieweeName"
            };
            var actualIntervieweeName = "Second IntervieweeName";
            var urlIntervieweeNameValidator = new UrlIntervieweeNameValidator(actualIntervieweeName);

            var validationResult = urlIntervieweeNameValidator.Validate(url);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_IntervieweeNamesAreTheSame_IsValid()
        {
            var intervieweeName = "IntervieweeName";
            var url = new Entities.Url
            {
                IntervieweeName = intervieweeName
            };
            var urlIntervieweeNameValidator = new UrlIntervieweeNameValidator(actualIntervieweeName: intervieweeName);

            var validationResult = urlIntervieweeNameValidator.Validate(url);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
