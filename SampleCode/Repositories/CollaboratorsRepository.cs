using System;

namespace SampleCode.Repositories
{
    public interface ICollaboratorsRepository
    {
        bool Exists(Guid id);
    }
}
