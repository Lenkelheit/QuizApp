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
        public void Validate_TestEventPropertiesAreDefault_ReturnsErrors()
        {
            var newTestEventDto = new NewTestEventDto
            {
                SessionId = default,
                EventType = default,
                Payload = default
            };
            var newTestEventDtoValidator = new NewTestEventDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Session Id is mandatory in test event.",
                "Payload is mandatory in test event."
            };

            var validationResult = newTestEventDtoValidator.Validate(newTestEventDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_TestEventPropertiesHaveDifferentErrors_ReturnsErrors()
        {
            var newTestEventDto = new NewTestEventDto
            {
                SessionId = Guid.NewGuid(),
                EventType = (Entities.Enums.EventType)2,
                Payload = "p"
            };
            var newTestEventDtoValidator = new NewTestEventDtoValidator();
            var expectedErrorMessages = new string[]
            {
                "Event Type doesn't contain such element in test event.",
                "Payload must be at least 10 characters in test event."
            };

            var validationResult = newTestEventDtoValidator.Validate(newTestEventDto);
            var actualErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();

            Assert.IsFalse(validationResult.IsValid);
            CollectionAssert.AreEqual(expected: expectedErrorMessages, actualErrorMessages);
        }

        [TestMethod]
        public void Validate_TestEventPropertiesAreWithoutErrors_NotReturnErrors()
        {
            var newTestEventDto = new NewTestEventDto
            {
                SessionId = Guid.NewGuid(),
                EventType = Entities.Enums.EventType.TestStarted,
                Payload = "012345678910"
            };
            var newTestEventDtoValidator = new NewTestEventDtoValidator();
            var expectedErrorMessagesCount = 0;

            var validationResult = newTestEventDtoValidator.Validate(newTestEventDto);
            var actualErrorMessagesCount = validationResult.Errors.Select(e => e.ErrorMessage).Count();

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(expected: expectedErrorMessagesCount, actualErrorMessagesCount);
        }
    }
}
