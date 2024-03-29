﻿using MediatR;

namespace Eice.Payment.Domain.Oferta.Commands
{
    public class OfertaEditCommand : Command, IRequest<bool>
    {
        public string PartnerId { get; set; }
        public string OfertaId { get; set; }
        public string CustomerIdAccepted { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new OfertaEditCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
