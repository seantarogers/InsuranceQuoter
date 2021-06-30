namespace InsuranceQuoter.Acl.Insurer.Service.MessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using NServiceBus;

    public class MnoInsurerQuoteRequestHandler : IHandleMessages<MnoInsurerQuoteRequest>
    {
        public Task Handle(MnoInsurerQuoteRequest message, IMessageHandlerContext context)
        {
            //To simulate latency
            Thread.Sleep(800);
            return context.Reply(
                new QuoteResponse
                {
                    CorrelationId = message.CorrelationId,
                    Uid = Guid.NewGuid(),
                    Insurer = "Mno Insurer and Company",
                    Premium = 1700M,
                    StartDate = DateTime.Now,
                    PremiumTax = 170M,
                    Addons = new List<string>
                    {
                        "Hire car cover",
                        "Personal accident cover",
                    }
                });
        }
    }
}