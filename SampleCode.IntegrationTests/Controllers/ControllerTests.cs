using System;
using NUnit.Framework;
using SampleCode.Controllers;
using SampleCode.IntegrationTests.Mocks.Models;
using SampleCode.IntegrationTests.Mocks.Repositories;
using SampleCode.Models;
using SampleCode.Validators;

namespace SampleCode.IntegrationTests.Controllers
{
    public class ControllerTests
    {
        private Controller _controller;

        [SetUp]
        public void Setup()
        {
            var repository = new MockCollaboratorsRepository();

            var validator = new ModelValidator(repository);

            _controller = new Controller(validator);
        }

        [Test]
        public void Method_CorrectModel_ReturnsOk()
        {
            const string expectedOutput = "1e8d132d-0d0a-4bc6-a8ec-177d5090a317 → \"Something\"";
            var correctModel = new Model
            {
                Id = new Guid("1e8d132d-0d0a-4bc6-a8ec-177d5090a317"),
                Content = "Something",
            };

            var actualResult = _controller.Method(correctModel);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Status, Is.EqualTo("OK"));
                Assert.That(actualResult.ErrorMessage, Is.Empty);
                Assert.That(actualResult.Output, Is.EqualTo(expectedOutput));
            });
        }

        [Test]
        public void Method_CorrectModelWithCollaborators_ReturnsOk()
        {
            const string expectedOutput = "1e8d132d-0d0a-4bc6-a8ec-177d5090a317, bd680e43-990b-421a-a395-9e0c0130f6a3, 58363f0d-5d4d-46cb-bc5f-9550228b67f0 → \"Something\"";
            var correctModel = new Model
            {
                Id = new Guid("1e8d132d-0d0a-4bc6-a8ec-177d5090a317"),
                Content = "Something",
                Collaborators = new[]
                {
                    new Guid("bd680e43-990b-421a-a395-9e0c0130f6a3"),
                    new Guid("58363f0d-5d4d-46cb-bc5f-9550228b67f0"),
                }
            };

            var actualResult = _controller.Method(correctModel);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Status, Is.EqualTo("OK"));
                Assert.That(actualResult.ErrorMessage, Is.Empty);
                Assert.That(actualResult.Output, Is.EqualTo(expectedOutput));
            });
        }

        [TestCaseSource(typeof(InvalidModels))]
        public void Method_IncorrectModel_ReturnsBad(Model incorrectModel, string expectedErrorMessage)
        {
            var actualResult = _controller.Method(incorrectModel);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Status, Is.EqualTo("Bad"));
                Assert.That(actualResult.ErrorMessage, Is.EqualTo(expectedErrorMessage));
                Assert.That(actualResult.Output, Is.Empty);
            });

        }
    }
}