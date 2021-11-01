using Bogus;
using Eice.Payment.API.Notification;
using Eice.Payment.Domain.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using Xunit;

namespace Eice.Payment.Test
{
    public class PraticeTest
    {
        protected readonly IMediator _mediator;
        private readonly ExceptionNotificationHandler _notifications;

        public PraticeTest(IMediator mediator, INotificationHandler<ExceptionNotification> notifications)
        {
            _mediator = mediator;
            _notifications = (ExceptionNotificationHandler)notifications;
        }

        private CustomerEntity FakeCustomer()
        {
            var faker = new Faker<CustomerEntity>("pt_BR").StrictMode(true)
                //.RuleFor(p => p.Cpf, f => f.Person.Cpf())
                .RuleFor(p => p.Cpf, f => f.Random.String(11, '0', '9'))
                .RuleFor(p => p.PartnerId, f => f.Random.ListItem(new List<string> { "616ca9b3f4176aff74373596", "616f6cddeee734e25c7b29ed" }));
            //.RuleFor(p => p.IpDeAcesso, f => f.Internet.IpAddress().ToString())
            //.RuleFor(p => p.NumeroDaConta, f => f.Finance.Account())
            //.RuleFor(p => p.Saldo, f => f.Finance.Amount(1, 10000));
            return faker.Generate();
        }

        [Fact]
        public void CreateCustomers()
        {

        }

        [Fact]
        public void CreateOfertas()
        {

        }


        [Fact]
        public void SortCoins()
        {

        }
    }
}
