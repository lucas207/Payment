using FluentValidation;

namespace Eice.Payment.API.Command
{
    public class ClientCreateCommandValidation : AbstractValidator<ClientCreateCommand>
    {
        public ClientCreateCommandValidation()
        {
            RuleFor(client => client.Name).NotNull();
            //VALIDAR FORMATO CPFCNPJ
        }
    }
}
