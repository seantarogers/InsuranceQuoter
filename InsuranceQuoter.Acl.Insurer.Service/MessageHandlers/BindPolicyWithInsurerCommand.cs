namespace InsuranceQuoter.Acl.Insurer.Service.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using NServiceBus;

    public class BindPolicyWithInsurerCommandHandler : IHandleMessages<BindPolicyWithInsurerCommand>
    {
        public Task Handle(BindPolicyWithInsurerCommand message, IMessageHandlerContext context)
        {
            var insurerGeneratedPolicyUid = Guid.NewGuid();

            return context.Publish(
                new PolicyBoundEvent()
                {
                    CorrelationId = message.CorrelationId,
                    PolicyUid = insurerGeneratedPolicyUid
                });
        }
    }
}