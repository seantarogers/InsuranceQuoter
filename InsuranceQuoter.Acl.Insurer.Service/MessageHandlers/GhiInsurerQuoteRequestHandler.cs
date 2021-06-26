namespace InsuranceQuoter.Acl.Insurer.Service.MessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using NServiceBus;

    public class GhiInsurerQuoteRequestHandler : IHandleMessages<GhiInsurerQuoteRequest>
    {
        public Task Handle(GhiInsurerQuoteRequest message, IMessageHandlerContext context)
        {
            Thread.Sleep(500);

            return context.Reply(
                new QuoteResponse
                {
                    CorrelationId = message.CorrelationId,
                    Uid = Guid.NewGuid(),
                    Insurer = "Ghi Insurer PLC",
                    Premium = 950M,
                    StartDate = DateTime.Now,
                    PremiumTax = 95M,
                    Addons = new List<string>()
                    {
                        "Car excess cover",
                        "Key cover",
                    }
                });
        }
    }
}