using FluentValidation.Results;

namespace Eice.Payment.Domain
{
    public abstract class Command
    {
        protected ValidationResult ValidationResult { get; set; }

        public ValidationResult GetValidationResult()
            => ValidationResult;

        public abstract bool IsValid();
    }
}
