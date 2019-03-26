using NUnit.Framework;
using SampleCode.Models;

namespace SampleCode.UnitTests.Models
{
    public class ValidationResultTests
    {
        [Test]
        public void Constructor_MissingErrorsCollection_ThrowsException()
            => Assert.That(
                () => new ValidationResult<TestModel>(null, null),
                Throws
                    .ArgumentNullException
                    .With
                    .Property("ParamName")
                    .EqualTo("errors"));

        // ReSharper disable once ClassNeverInstantiated.Local
        private class TestModel
        {
        }
    }
}
