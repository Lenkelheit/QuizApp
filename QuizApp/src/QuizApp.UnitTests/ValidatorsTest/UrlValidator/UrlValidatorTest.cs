using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuizApp.UnitTests.ValidatorsTest.UrlValidator
{
    [TestClass]
    public class UrlValidatorTest
    {
        [TestMethod]
        public void Validate_UrlPropertiesAreDefault_ReturnsErrors()
        {
            var url = new Entities.Url
            {
                ValidFromTime = default,
                ValidUntilTime = default,
                NumberOfRuns = default
            };
            var urlValidator = new BLL.Validators.UrlValidator.UrlValidator();
            var expectedErrorMessages = new string[]
            {
                $"The test ended on {url.ValidUntilTime}."
            };

            var validationResult = urlValidator.Validate(url);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_UrlPropertiesHaveDifferentErrors_ReturnsErrors()
        {
            var nowTime = DateTime.Now;
            var url = new Entities.Url
            {
                ValidFromTime = new DateTime(nowTime.Ticks + 1000),
                ValidUntilTime = new DateTime(nowTime.Ticks - 1000),
                NumberOfRuns = 0
            };
            var urlValidator = new BLL.Validators.UrlValidator.UrlValidator();
            var expectedErrorMessages = new string[]
            {
                $"The test will start on {url.ValidFromTime}.",
                $"The test ended on {url.ValidUntilTime}.",
                "The number of runs of this test is over."
            };

            var validationResult = urlValidator.Validate(url);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_UrlPropertiesAreWithoutErrors_NotReturnErrors()
        {
            var nowTime = DateTime.Now;
            var url = new Entities.Url
            {
                ValidFromTime = new DateTime(nowTime.Ticks - 1000),
                ValidUntilTime = new DateTime(nowTime.Ticks + 1000),
                NumberOfRuns = 1
            };
            var urlValidator = new BLL.Validators.UrlValidator.UrlValidator();
            var expectedErrorMessagesCount = 0;

            var validationResult = urlValidator.Validate(url);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
