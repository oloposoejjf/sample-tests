using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SampleCode.Models;

namespace SampleCode.IntegrationTests.Mocks.Models
{
    internal class InvalidModels : IEnumerable<TestCaseData>
    {
        public IEnumerator<TestCaseData> GetEnumerator()
        {
            yield return new TestCaseData(
                null,
                $"No Model provided.{Environment.NewLine}Content is missing, provide non-empty content, please.{Environment.NewLine}Collaborators are missing, if there are none, provide an empty collection."
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid(),
                    Content = "something",
                },
                "No Id provided."
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("fa83d630-e51e-4fc4-bac3-ea38ed5c0246"),
                    Content = "something",
                    Collaborators = null,
                },
                "Collaborators are missing, if there are none, provide an empty collection."
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("abdcc6da-f88e-4b0b-9fe4-c3acffa3ec87"),
                    Content = null,
                },
                "Content is missing, provide non-empty content, please."
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("6fb6ef6b-c291-4786-a82b-4a3121c54c36"),
                    Content = "",
                    Collaborators = new[]
                    {
                        new Guid("58363f0d-5d4d-46cb-bc5f-9550228b67f0"),
                    },
                },
                "Content is empty, provide non-empty content, please."
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("353a88fb-26a5-48a2-82e5-726502ac3e33"),
                    Content = "  ",
                    Collaborators = new[]
                    {
                        new Guid("bd680e43-990b-421a-a395-9e0c0130f6a3"),
                        new Guid("14ab02fb-e289-432f-91ae-a2cd65fe5084"),
                    },
                },
                "Content consists of white space characters only, provide visually non-empty content, please."
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("ea1d9c05-8fdc-4d62-b9ff-a60c58076f1d"),
                    Content = "Everybody goes shit, fuck",
                    Collaborators = new[]
                    {
                        new Guid("58363f0d-5d4d-46cb-bc5f-9550228b67f0"),
                        new Guid("14ab02fb-e289-432f-91ae-a2cd65fe5084"),
                    },
                },
                "Content contains some forbidden swear words. Be polite, please."
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = new Guid("f9b5fd5a-6303-4aa9-bf73-5152eedecd73"),
                    Content = "Something nonempty",
                    Collaborators = new[]
                    {
                        new Guid("caa8f6b6-dd0d-47fb-a8fc-100b86dff4c6"),
                        new Guid("bd680e43-990b-421a-a395-9e0c0130f6a3"),
                    },
                },
                "Some Collaborators cannot be found."
            );

            yield return new TestCaseData(
                new Model
                {
                    Id = Guid.Empty,
                    Content = "Everybody goes shit, fuck",
                    Collaborators = new[]
                    {
                        new Guid("a9f915f6-b4fe-4292-baf7-57836523ed38"),
                    },
                },
                $"No Id provided.{Environment.NewLine}Content contains some forbidden swear words. Be polite, please.{Environment.NewLine}Some Collaborators cannot be found."
            );
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}