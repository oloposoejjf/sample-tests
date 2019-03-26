using System;
using System.Collections.Generic;

namespace SampleCode.Models
{
    internal class Result
    {
        public string Status { get; }

        public string ErrorMessage { get; } = string.Empty;

        public string Output { get; } = string.Empty;

        public Result(Model validResult)
        {
            Output = validResult.ToString();
            Status = "OK";
        }

        public Result(IEnumerable<string> errors)
        {
            Status = "Bad";
            ErrorMessage = string.Join(Environment.NewLine, errors);
        }
    }
}