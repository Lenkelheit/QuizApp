using System;
using System.Collections.Generic;
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
        public void Validate_UrlPropertiesAreDefault_IsNotValid()
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

            var validationResult = newUrlDtoValidator.Validate(newUrlDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_UrlPropertiesHaveDifferentErrors_IsNotValid()
        {
            var newUrlDto = new NewUrlDto
            {
                NumberOfRuns = -1,
                ValidFromTime = DateTime.Now.AddHours(3),
                ValidUntilTime = DateTime.Now,
                IntervieweeName = "i",
                TestId = 0
            };
            var newUrlDtoValidator = new NewUrlDtoValidator();

            var validationResult = newUrlDtoValidator.Validate(newUrlDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_UrlPropertiesAreWithoutErrors_IsValid()
        {
            var newUrlDto = new NewUrlDto
            {
                NumberOfRuns = 0,
                ValidFromTime = DateTime.Now,
                ValidUntilTime = DateTime.Now.AddHours(3),
                IntervieweeName = "IntervieweeName",
                TestId = 1
            };
            var newUrlDtoValidator = new NewUrlDtoValidator();

            var validationResult = newUrlDtoValidator.Validate(newUrlDto);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
