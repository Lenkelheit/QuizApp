using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuizApp.BLL.Dto.TestEvent;
using QuizApp.Web.Validators.TestEvent;

namespace QuizApp.UnitTests.ValidatorsTest.TestEvent
{
    [TestClass]
    public class NewTestEventDtoValidatorTest
    {
        [TestMethod]
        public void Validate_TestEventPropertiesAreDefault_IsNotValid()
        {
            var newTestEventDto = new NewTestEventDto
            {
                SessionId = default,
                EventType = default,
                Payload = default
            };
            var newTestEventDtoValidator = new NewTestEventDtoValidator();

            var validationResult = newTestEventDtoValidator.Validate(newTestEventDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_TestEventPropertiesHaveDifferentErrors_IsNotValid()
        {
            var newTestEventDto = new NewTestEventDto
            {
                SessionId = Guid.NewGuid(),
                EventType = (Entities.Enums.EventType)2,
                Payload = "p"
            };
            var newTestEventDtoValidator = new NewTestEventDtoValidator();

            var validationResult = newTestEventDtoValidator.Validate(newTestEventDto);

            Assert.IsFalse(validationResult.IsValid);
        }

        [TestMethod]
        public void Validate_TestEventPropertiesAreWithoutErrors_IsValid()
        {
            var newTestEventDto = new NewTestEventDto
            {
                SessionId = Guid.NewGuid(),
                EventType = Entities.Enums.EventType.TestStarted,
                Payload = "012345678910"
            };
            var newTestEventDtoValidator = new NewTestEventDtoValidator();

            var validationResult = newTestEventDtoValidator.Validate(newTestEventDto);

            Assert.IsTrue(validationResult.IsValid);
        }
    }
}
