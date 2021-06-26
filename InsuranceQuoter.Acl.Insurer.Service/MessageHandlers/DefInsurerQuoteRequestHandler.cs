namespace InsuranceQuoter.Acl.Insurer.Service.MessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using NServiceBus;

    public class DefInsurerQuoteRequestHandler : IHandleMessages<DefInsurerQuoteRequest>
    {
        public Task Handle(DefInsurerQuoteRequest message, IMessageHandlerContext context)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            return context.Reply(
                new QuoteResponse()
                {
                    CorrelationId = message.CorrelationId,
                    Uid = Guid.NewGuid(),
                    Insurer = "Def Insurer PLC",
                    Premium = 900M,
                    StartDate = DateTime.Now,
                    PremiumTax = 90M,
                    Addons = new List<string>()
                    {
                        "Key cover",
                        "Car Excess cover",
                        "Hire Car cover"
                    }
                });
        }
    }
}