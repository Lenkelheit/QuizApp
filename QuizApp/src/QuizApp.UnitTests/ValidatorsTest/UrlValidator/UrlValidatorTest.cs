using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuizApp.UnitTests.ValidatorsTest.UrlValidator
{
    [TestClass]
    public class UrlValidatorTest
    {
        [TestMethod]
        public void Validate_UrlPropertiesAreDefault_IsNotValid()
        {
            var url = new Entities.Url
            {
                ValidFromTime = default,
                ValidUntilTime = default,
                NumberOfRuns = default
            };
            var urlValidator = new BLL.Validators.UrlValidator.UrlValidator();

            var validationResult = urlValidator.Validate(url);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_UrlPropertiesHaveDifferentErrors_IsNotValid()
        {
            var url = new Entities.Url
            {
                ValidFromTime = DateTime.Now.AddHours(1),
                ValidUntilTime = DateTime.Now.AddHours(-1),
                NumberOfRuns = 0
            };
            var urlValidator = new BLL.Validators.UrlValidator.UrlValidator();

            var validationResult = urlValidator.Validate(url);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_UrlPropertiesAreWithoutErrors_IsValid()
        {
            var url = new Entities.Url
            {
                ValidFromTime = DateTime.Now.AddHours(-1),
                ValidUntilTime = DateTime.Now.AddHours(1),
                NumberOfRuns = 1
            };
            var urlValidator = new BLL.Validators.UrlValidator.UrlValidator();

            var validationResult = urlValidator.Validate(url);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
