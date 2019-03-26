using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Models
{
    public class Model
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public IEnumerable<Guid> Collaborators { get; set; } = Enumerable.Empty<Guid>();

        public override string ToString()
            => $"{string.Join(", ", Collaborators?.Prepend(Id) ?? new[] {Id})} → \"{Content}\"";
    }
}
