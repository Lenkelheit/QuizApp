using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.Test;
using QuizApp.Web.Validators.Test;

namespace QuizApp.UnitTests.ValidatorsTest.Test
{
    [TestClass]
    public class NewTestDtoValidatorTest
    {
        [TestMethod]
        public void Validate_TestPropertiesAreDefault_IsNotValid()
        {
            var newTestDto = new NewTestDto
            {
                Title = default,
                Description = default,
                TimeLimitSeconds = default,
                AuthorId = default
            };
            var newTestDtoValidator = new NewTestDtoValidator();

            var validationResult = newTestDtoValidator.Validate(newTestDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_TestPropertiesAreNotDefaultButHaveBadLength_IsNotValid()
        {
            var newTestDto = new NewTestDto
            {
                Title = "t",
                Description = "des",
                TimeLimitSeconds = TimeSpan.FromSeconds(1),
                AuthorId = 1
            };
            var newTestDtoValidator = new NewTestDtoValidator();

            var validationResult = newTestDtoValidator.Validate(newTestDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_TestPropertiesAreNotDefaultAndHaveGoodLength_IsValid()
        {
            var newTestDto = new NewTestDto
            {
                Title = "Title",
                Description = "Description",
                TimeLimitSeconds = TimeSpan.FromSeconds(1),
                AuthorId = 1
            };
            var newTestDtoValidator = new NewTestDtoValidator();

            var validationResult = newTestDtoValidator.Validate(newTestDto);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
