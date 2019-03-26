using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SampleCode.Models;
using SampleCode.Repositories;

namespace SampleCode.Validators
{
    public interface IValidator<TModel>
        where TModel : class
    {
        IValidationResult<TModel> Validate(TModel model);
    }

    internal class ModelValidator : IValidator<Model>
    {
        private static readonly ISet<string> SwearWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "fuck",
            "shit",
            "poop",
            "boob"
        };

        private readonly ICollaboratorsRepository _collaborators;

        public ModelValidator(ICollaboratorsRepository collaborators)
            => _collaborators = collaborators;

        public IValidationResult<Model> Validate(Model model)
        {
            var errors = Enumerable
                .Empty<string>()
                .Union(ValidateConsistency(model))
                .Union(ValidateContent(model))
                .Union(ValidateCollaborators(model))
                .ToImmutableArray();

            return new ValidationResult<Model>(model, errors);
        }

        private static IEnumerable<string> ValidateConsistency(Model model)
        {
            if(model == null)
                yield return $"No {nameof(Model)} provided.";

            if (model != null && model.Id == Guid.Empty)
                yield return $"No {nameof(Model.Id)} provided.";
        }

        private static IEnumerable<string> ValidateContent(Model model)
        {
            if(model?.Content == null)
                yield return $"{nameof(Model.Content)} is missing, provide non-empty content, please.";

            if(model?.Content == string.Empty)
                yield return $"{nameof(Model.Content)} is empty, provide non-empty content, please.";

            if (!string.IsNullOrEmpty(model?.Content) && string.IsNullOrWhiteSpace(model.Content))
                yield return $"{nameof(Model.Content)} consists of white space characters only, provide visually non-empty content, please.";

            var areAnySwearWordsPresent = model?
                    .Content?
                    .Split(" ")
                    .Any(SwearWords.Contains)
                ?? false;
            if (areAnySwearWordsPresent)
                yield return $"{nameof(Model.Content)} contains some forbidden swear words. Be polite, please.";
        }

        private IEnumerable<string> ValidateCollaborators(Model model)
        {
            if (model?.Collaborators == null)
                yield return $"{nameof(Model.Collaborators)} are missing, if there are none, provide an empty collection.";

            var areAnyCollaboratorsMissing = model?
                .Collaborators?
                .Where(collaboratorId => !_collaborators.Exists(collaboratorId))
                .Any()
                ?? false;
            if (areAnyCollaboratorsMissing)
                yield return $"Some {nameof(Model.Collaborators)} cannot be found.";
        }

    }
}
