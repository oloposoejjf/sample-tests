using System;
using System.Runtime.CompilerServices;
using SampleCode.Controllers;

[assembly: InternalsVisibleTo("SampleCode.UnitTests")]
[assembly: InternalsVisibleTo("SampleCode.IntegrationTests")]

namespace SampleCode
{
    internal class Program
    {
        private static void Main(string[] args)
            => throw new InvalidOperationException($"Run tests, see {nameof(Controller)}.");
    }
}
