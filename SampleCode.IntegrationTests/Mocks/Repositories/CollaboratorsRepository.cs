using System;
using System.Collections.Generic;
using SampleCode.Repositories;

namespace SampleCode.IntegrationTests.Mocks.Repositories
{
    internal class MockCollaboratorsRepository : ICollaboratorsRepository
    {
        private static readonly ISet<Guid> Collaborators = new HashSet<Guid>
        {
            new Guid("bd680e43-990b-421a-a395-9e0c0130f6a3"),
            new Guid("58363f0d-5d4d-46cb-bc5f-9550228b67f0"),
            new Guid("14ab02fb-e289-432f-91ae-a2cd65fe5084"),
        };

        public bool Exists(Guid id)
            => Collaborators.Contains(id);
    }
}
