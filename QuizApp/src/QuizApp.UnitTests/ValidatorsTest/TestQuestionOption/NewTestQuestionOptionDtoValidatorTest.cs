using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.TestQuestionOption;
using QuizApp.Web.Validators.TestQuestionOption;

namespace QuizApp.UnitTests.ValidatorsTest.TestQuestionOption
{
    [TestClass]
    public class NewTestQuestionOptionDtoValidatorTest
    {
        [TestMethod]
        public void Validate_QuestionOptionPropertiesAreDefault_IsNotValid()
        {
            var newTestQuestionOptionDto = new NewTestQuestionOptionDto
            {
                Text = default,
                IsRight = default
            };
            var newTestQuestionOptionDtoValidator = new NewTestQuestionOptionDtoValidator();

            var validationResult = newTestQuestionOptionDtoValidator.Validate(newTestQuestionOptionDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_QuestionOptionPropertiesAreNotDefaultButHaveBadLength_IsNotValid()
        {
            var newTestQuestionOptionDto = new NewTestQuestionOptionDto
            {
                Text = "t",
                IsRight = true
            };
            var newTestQuestionOptionDtoValidator = new NewTestQuestionOptionDtoValidator();

            var validationResult = newTestQuestionOptionDtoValidator.Validate(newTestQuestionOptionDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_QuestionOptionPropertiesAreNotDefaultAndHaveGoodLength_IsValid()
        {
            var newTestQuestionOptionDto = new NewTestQuestionOptionDto
            {
                Text = "Text",
                IsRight = true
            };
            var newTestQuestionOptionDtoValidator = new NewTestQuestionOptionDtoValidator();

            var validationResult = newTestQuestionOptionDtoValidator.Validate(newTestQuestionOptionDto);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
