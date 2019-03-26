using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using SampleCode.Models;
using SampleCode.Repositories;
using SampleCode.UnitTests.Mocks.Models;
using SampleCode.Validators;

namespace SampleCode.UnitTests.Validators
{
    public class ModelValidatorTests
    {
        private ICollaboratorsRepository _repository;
        private ModelValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<ICollaboratorsRepository>();
            _validator = new ModelValidator(_repository);
        }

        [TestCaseSource(typeof(ValidModels))]
        public void Validate_CorrectModel_ReturnsValidResult(Model validModel)
        {
            PrefillRepository(ValidModels.ExistingCollaborators);

            var actualResult = _validator.Validate(validModel);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.IsValid, Is.True);
                Assert.That(actualResult.Errors, Is.Empty);
                Assert.That(actualResult.Model, Is.EqualTo(validModel));
            });
        }

        [TestCaseSource(typeof(InvalidModels))]
        public void Validate_IncorrectModel_ReturnsInvalidResult(Model invalidModel, IEnumerable<string> expectedErrors)
        {
            PrefillRepository(InvalidModels.ExistingCollaborators);

            var actualResult = _validator.Validate(invalidModel);

            Assert.Multiple(() =>
            {
                Assert.That(actualResult.IsValid, Is.False);
                Assert.That(actualResult.Errors, Is.EquivalentTo(expectedErrors));
                Assert.That(actualResult.Model, Is.EqualTo(invalidModel));
            });
        }

        private void PrefillRepository(ICollection<Guid> collaborators)
            => _repository
                .Exists(Arg.Any<Guid>())
                .Returns(info => collaborators.Contains(info.Arg<Guid>()));
    }
}
