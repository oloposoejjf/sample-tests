using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleCode.Models
{
    public interface IValidationResult<out TModel>
        where TModel : class
    {
        IEnumerable<string> Errors { get; }
        TModel Model { get; }
        bool IsValid { get; }
    }

    internal class ValidationResult<TModel> : IValidationResult<TModel> where TModel : class
    {
        public IEnumerable<string> Errors { get; }

        public TModel Model { get; }

        public bool IsValid => !Errors.Any();

        public ValidationResult(TModel model, IEnumerable<string> errors)
        {
            Model = model;
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }
    }
}
