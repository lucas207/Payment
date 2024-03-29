﻿using MediatR;

namespace Eice.Payment.Domain.Customer.Commands
{
    public class CustomerEditCommand : Command, IRequest<bool>
    {
        public string PartnerId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CustomerEditCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
