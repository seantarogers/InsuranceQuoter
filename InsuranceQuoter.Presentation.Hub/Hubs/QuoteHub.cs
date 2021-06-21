namespace InsuranceQuoter.Presentation.Hub.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using Microsoft.AspNetCore.SignalR;
    using NServiceBus;

    public class QuoteHub : Hub
    {
        private readonly IMessageSession messageSession;

        public QuoteHub(IMessageSession messageSession)
        {
            this.messageSession = messageSession;
        }

        public async Task HandleTakePaymentCommandAsync(TakePaymentCommand command)
        {
            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "PaymentProviderContactedEventHandler",
                new PaymentProviderContactedEvent()
            );

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "CardAuthorisedEventHandler",
                new CardAuthorisedEvent()
                {
                    CardNumber = command.CardNumber
                });

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "PaymentTakenEventHandler",
                new PaymentTakenEvent
                {
                    CardNumber = command.CardNumber,
                    PaymentUid = Guid.NewGuid()
                });

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "InsurerContactedEventHandler",
                new InsurerContactedEvent());

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "PolicyBoundEventHandler",
                new PolicyBoundEvent
                {
                    Reference = Guid.NewGuid().ToString()
                });
        }

        public async Task HandleQuotesRequestAsync(QuotesRequest request)
        {
            await messageSession.Send(request, new SendOptions());

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "QuotesResponseHandler",
                new QuoteResponse()
                {
                    Uid = Guid.NewGuid(),
                    Insurer = "Insurer1",
                    Premium = 100M,
                    StartDate = DateTime.Now,
                    PremiumTax = 10M,
                    Addons = new List<string>()
                    {
                        "Key cover"
                    }
                });
            await Clients.All.SendAsync(
                "QuotesResponseHandler",
                new QuoteResponse()
                {
                    Uid = Guid.NewGuid(),
                    Insurer = "Insurer5",
                    Premium = 500M,
                    PremiumTax = 50M,
                    StartDate = DateTime.Now,
                    Addons = new List<string>()
                    {
                        "Key cover"
                    }
                });

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "QuotesResponseHandler",
                new QuoteResponse()
                {
                    Uid = Guid.NewGuid(),
                    Insurer = "Insurer2",
                    Premium = 200M,
                    PremiumTax = 20M,
                    StartDate = DateTime.Now,
                    Addons = new List<string>()
                    {
                        "Key cover",
                        "Break down cover"
                    }
                });

            Thread.Sleep(1000);
            await Clients.All.SendAsync(
                "QuotesResponseHandler",
                new QuoteResponse()
                {
                    Uid = Guid.NewGuid(),
                    Insurer = "Insurer3",
                    Premium = 300M,
                    PremiumTax = 30M,
                    StartDate = DateTime.Now,
                    Addons = new List<string>()
                    {
                        "Break down cover"
                    }
                });
            await Clients.All.SendAsync(
                "QuotesResponseHandler",
                new QuoteResponse()
                {
                    Uid = Guid.NewGuid(),
                    Insurer = "Insurer4",
                    Premium = 400M,
                    PremiumTax = 40M,
                    StartDate = DateTime.Now,
                    Addons = new List<string>()
                    {
                        "No claims protection"
                    }
                });
        }
    }
}