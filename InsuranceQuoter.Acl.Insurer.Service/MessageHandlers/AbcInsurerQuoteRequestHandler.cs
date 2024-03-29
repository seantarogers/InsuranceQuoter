﻿namespace InsuranceQuoter.Acl.Insurer.Service.MessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Infrastructure.Message.Responses;
    using NServiceBus;

    public class AbcInsurerQuoteRequestHandler : IHandleMessages<AbcInsurerQuoteRequest>
    {
        public Task Handle(AbcInsurerQuoteRequest message, IMessageHandlerContext context)
        {
            //To simulate latency
            Thread.Sleep(1100);

            return context.Reply(
                new QuoteResponse
                {
                    CorrelationId = message.CorrelationId,
                    Uid = Guid.NewGuid(),
                    Insurer = "Abc Insurer Ltd",
                    Premium = 875M,
                    StartDate = DateTime.Now,
                    PremiumTax = 87M,
                    Addons = new List<string>()
                    {
                        "Key cover",
                        "Legal cover",
                        "Breakdown cover"
                    }
                });
        }
    }
}