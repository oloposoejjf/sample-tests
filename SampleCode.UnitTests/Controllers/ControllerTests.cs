using System;
using NSubstitute;
using NUnit.Framework;
using SampleCode.Controllers;
using SampleCode.Models;
using SampleCode.Validators;

namespace SampleCode.UnitTests.Controllers
{
    public class ControllerTests
    {
        private Controller _controller;
        private IValidator<Model> _validator;

        [SetUp]
        public void Setup()
        {
            _validator = Substitute.For<IValidator<Model>>();
            _controller = new Controller(_validator);
        }

        [Test]
        public void Method_CorrectModel_ReturnsOK()
        {
            var (correctModel, expectedOutput) = CreateCorrectModel("1e8d132d-0d0a-4bc6-a8ec-177d5090a317", "something");
            SetupValidatorForCorrectResult(correctModel);

            var actualResult = _controller.Method(correctModel);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Status, Is.EqualTo("OK"));
                Assert.That(actualResult.ErrorMessage, Is.Empty);
                Assert.That(actualResult.Output, Is.EqualTo(expectedOutput));
            });
        }

        [Test]
        public void Method_IncorrectModel_ReturnsBad()
        {
            var expectedErrorMessage = $"Something{Environment.NewLine}Went{Environment.NewLine}Wrong";
            SetupValidatorForIncorrectResult(expectedErrorMessage);

            var actualResult = _controller.Method(new Model());

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.Status, Is.EqualTo("Bad"));
                Assert.That(actualResult.Output, Is.Empty);
                Assert.That(actualResult.ErrorMessage, Is.EqualTo(expectedErrorMessage));
            });
        }

        private (Model, string) CreateCorrectModel(string guid, string content)
            => (
                new Model
                {
                    Id = new Guid(guid),
                    Content = content,
                    Collaborators = new Guid[0],
                },
                $"{guid} → \"{content}\""
            );

        private void SetupValidatorForCorrectResult(Model correctModel)
        {
            var validatedModel = Substitute.For<IValidationResult<Model>>();
            validatedModel.IsValid.Returns(true);
            validatedModel.Model.Returns(correctModel);

            _validator
                .Validate(correctModel)
                .Returns(validatedModel);
        }

        private void SetupValidatorForIncorrectResult(string expectedErrorMessage)
        {
            var validatedModel = Substitute.For<IValidationResult<Model>>();
            validatedModel.IsValid.Returns(false);
            validatedModel.Errors.Returns(expectedErrorMessage.Split(Environment.NewLine));

            _validator
                .Validate(Arg.Any<Model>())
                .Returns(validatedModel);
        }
    }
}