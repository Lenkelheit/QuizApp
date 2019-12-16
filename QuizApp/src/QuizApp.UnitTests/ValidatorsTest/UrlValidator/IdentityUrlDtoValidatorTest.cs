using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.UrlValidator;
using QuizApp.Web.Validators.UrlValidator;

namespace QuizApp.UnitTests.ValidatorsTest.UrlValidator
{
    [TestClass]
    public class IdentityUrlDtoValidatorTest
    {
        [TestMethod]
        public void Validate_IdentityUrlPropertiesAreDefault_ReturnsErrors()
        {
            var identityUrlDto = new IdentityUrlDto
            {
                Id = default,
                IntervieweeName = default
            };
            var identityUrlDtoValidator = new IdentityUrlDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Id is mandatory in url.",
                "Interviewee Name is mandatory in url."
            };

            var validationResult = identityUrlDtoValidator.Validate(identityUrlDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_IdentityUrlPropertiesHaveBadLength_ReturnsErrors()
        {
            var identityUrlDto = new IdentityUrlDto
            {
                Id = 1,
                IntervieweeName = "i"
            };
            var identityUrlDtoValidator = new IdentityUrlDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Interviewee Name must be from 4 to 128 characters in url."
            };

            var validationResult = identityUrlDtoValidator.Validate(identityUrlDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_IdentityUrlPropertiesAreWithoutErrors_NotReturnErrors()
        {
            var identityUrlDto = new IdentityUrlDto
            {
                Id = 1,
                IntervieweeName = "IntervieweeName"
            };
            var identityUrlDtoValidator = new IdentityUrlDtoValidator();
            var expectedErrorMessagesCount = 0;

            var validationResult = identityUrlDtoValidator.Validate(identityUrlDto);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
