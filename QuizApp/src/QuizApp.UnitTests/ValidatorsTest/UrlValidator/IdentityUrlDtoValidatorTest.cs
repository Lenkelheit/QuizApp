using System;
using System.Collections.Generic;
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
        public void Validate_IdentityUrlPropertiesAreDefault_IsNotValid()
        {
            var identityUrlDto = new IdentityUrlDto
            {
                Id = default,
                IntervieweeName = default
            };
            var identityUrlDtoValidator = new IdentityUrlDtoValidator();

            var validationResult = identityUrlDtoValidator.Validate(identityUrlDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_IdentityUrlPropertiesHaveBadLength_IsNotValid()
        {
            var identityUrlDto = new IdentityUrlDto
            {
                Id = 1,
                IntervieweeName = "i"
            };
            var identityUrlDtoValidator = new IdentityUrlDtoValidator();

            var validationResult = identityUrlDtoValidator.Validate(identityUrlDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_IdentityUrlPropertiesAreWithoutErrors_IsValid()
        {
            var identityUrlDto = new IdentityUrlDto
            {
                Id = 1,
                IntervieweeName = "IntervieweeName"
            };
            var identityUrlDtoValidator = new IdentityUrlDtoValidator();

            var validationResult = identityUrlDtoValidator.Validate(identityUrlDto);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
