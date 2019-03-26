using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SampleCode.Models;

namespace SampleCode.UnitTests.Mocks.Models
{
    internal class ValidModels : IEnumerable<Model>
    {
        internal static readonly ISet<Guid> ExistingCollaborators = new HashSet<Guid>
        {
            new Guid("2baeda74-6be8-4f39-a485-7510be10d829"),
            new Guid("caa8f6b6-dd0d-47fb-a8fc-100b86dff4c6"),
            new Guid("91b299ce-fe34-409e-94b7-453f2df1efdf")
        };

        public IEnumerator<Model> GetEnumerator()
        {
            yield return new Model
            {
                Id = new Guid("3af27608-efbf-45f8-9fa4-7c5a449ba9e4"),
                Content = "Something nonempty"
            };

            yield return new Model
            {
                Id = new Guid("9673ef6e-edc1-46de-9185-94ad99dfec4a"),
                Content = "Something nonempty",
                Collaborators = new List<Guid>(ExistingCollaborators.SkipLast(1))
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}