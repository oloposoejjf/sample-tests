using SampleCode.Models;
using SampleCode.Validators;

namespace SampleCode.Controllers
{
    internal class Controller
    {
        private readonly IValidator<Model> _validator;

        public Controller(IValidator<Model> validator)
            => _validator = validator;

        public Result Method(Model model)
        {
            var validatedModel = _validator.Validate(model);

            return validatedModel.IsValid
                ? new Result(validatedModel.Model)
                : new Result(validatedModel.Errors);
        }
    }
}