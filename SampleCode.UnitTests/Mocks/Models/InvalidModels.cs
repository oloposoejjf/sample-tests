using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SampleCode.Models;

namespace SampleCode.UnitTests.Mocks.Models
{
    internal class InvalidModels : IEnumerable<TestCaseData>
    {
        internal static readonly ISet<Guid> ExistingCollaborators = new HashSet<Guid>
        {
            new Guid("e8a915f6-b4fe-4292-baf7-57836523ed58"),
            new Guid("91b299ce-fe34-409e-94b7-453f2df1efdf")
        };

        public IEnumerator<TestCaseData> GetEnumerator()
        {
            yield return new TestCaseData(
                null,
                new[]
                {
                    "No Model provided.",
                    "Content is missing, provide non-empty content, please.",
                    "Collaborators are missing, if there are none, provide an empty collection."
                }
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid(),
                    Content = "something",
                },
                new[] {"No Id provided."}
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("4f59bb2b-748a-45e6-8946-6af01854be47"),
                    Content = null,
                },
                new[] {"Content is missing, provide non-empty content, please."}
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("4fe849da-be70-4514-bd4a-0a38c11d3b09"),
                    Content = "",
                    Collaborators = new[]
                    {
                        new Guid("e8a915f6-b4fe-4292-baf7-57836523ed58"),
                    },
                },
                new[] {"Content is empty, provide non-empty content, please."}
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("39d0ccdf-2e4b-4a05-9904-32bda0d85727"),
                    Content = "  ",
                    Collaborators = new[]
                    {
                        new Guid("e8a915f6-b4fe-4292-baf7-57836523ed58"),
                        new Guid("91b299ce-fe34-409e-94b7-453f2df1efdf")
                    },
                },
                new[] {"Content consists of white space characters only, provide visually non-empty content, please."}
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("35e7419f-530c-4a3e-a5c4-49a70ac07ecc"),
                    Content = "Everybody goes shit, fuck",
                    Collaborators = new[]
                    {
                        new Guid("e8a915f6-b4fe-4292-baf7-57836523ed58"),
                        new Guid("91b299ce-fe34-409e-94b7-453f2df1efdf")
                    },
                },
                new[] {"Content contains some forbidden swear words. Be polite, please."}
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("d05351b3-1d56-4383-b091-33b5920472ac"),
                    Content = "Something nonempty",
                    Collaborators = new[]
                    {
                        new Guid("caa8f6b6-dd0d-47fb-a8fc-100b86dff4c6"),
                        new Guid("91b299ce-fe34-409e-94b7-453f2df1efdf"),
                    },
                },
                new[] {"Some Collaborators cannot be found."}
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid(),
                    Content = "Everybody goes shit, fuck",
                    Collaborators = new[]
                    {
                        new Guid("a9f915f6-b4fe-4292-baf7-57836523ed38"),
                    },
                },
                new[]
                {
                    "No Id provided.",
                    "Content contains some forbidden swear words. Be polite, please.",
                    "Some Collaborators cannot be found."
                }
            );
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}