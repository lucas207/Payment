using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eice.Payment.API.Command
{
    public abstract class Command
    {
        protected ValidationResult ValidationResult { get; set; }

        public ValidationResult GetValidationResult()
            => ValidationResult;

        public abstract bool IsValid();
    }
}
