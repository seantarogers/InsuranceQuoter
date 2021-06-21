namespace InsuranceQuoter.Saga.Service.MessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using NServiceBus;

    public class QuotesRequestHandler : IHandleMessages<QuotesRequest>
    {
        public Task Handle(QuotesRequest message, IMessageHandlerContext context)
        {
            Console.WriteLine(message.CarId);

            return context.Reply(
                new QuoteResponse()
                {
                    Uid = Guid.NewGuid(),
                    Insurer = "From the Saga ",
                    Premium = 100M,
                    StartDate = DateTime.Now,
                    PremiumTax = 10M,
                    Addons = new List<string>()
                    {
                        "Key cover"
                    }
                });
        }
    }
}