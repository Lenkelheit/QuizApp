using System;
using System.Collections.Generic;
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
        public void Validate_TestResultPropertiesAreDefault_IsNotValid()
        {
            var newTestResultDto = new NewTestResultDto
            {
                IntervieweeName = default,
                PassingStartTime = default,
                PassingEndTime = default,
                Score = default
            };
            var newTestResultDtoValidator = new NewTestResultDtoValidator();

            var validationResult = newTestResultDtoValidator.Validate(newTestResultDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_TestResultPropertiesHaveDifferentErrors_IsNotValid()
        {
            var newTestResultDto = new NewTestResultDto
            {
                IntervieweeName = "i",
                PassingStartTime = DateTime.Now.AddHours(3),
                PassingEndTime = DateTime.Now,
                Score = 120
            };
            var newTestResultDtoValidator = new NewTestResultDtoValidator();

            var validationResult = newTestResultDtoValidator.Validate(newTestResultDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_TestResultPropertiesAreWithoutErrors_IsValid()
        {
            var newTestResultDto = new NewTestResultDto
            {
                IntervieweeName = "IntervieweeName",
                PassingStartTime = DateTime.Now,
                PassingEndTime = DateTime.Now.AddHours(3),
                Score = 100
            };
            var newTestResultDtoValidator = new NewTestResultDtoValidator();

            var validationResult = newTestResultDtoValidator.Validate(newTestResultDto);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
