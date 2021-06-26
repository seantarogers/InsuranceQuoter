namespace InsuranceQuoter.Domain.Service.MessageHandlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using NServiceBus;
    using DomainCommandHandler = InsuranceQuoter.Application.Command.Handlers;

    public class AddPolicyCommandHandler : IHandleMessages<AddPolicyCommand>
    {
        private readonly GetRiskByUidQueryHandler getRiskByUidQueryHandler;
        private readonly DomainCommandHandler.AddPolicyCommandHandler addPolicyCommandHandler;

        public AddPolicyCommandHandler(GetRiskByUidQueryHandler getRiskByUidQueryHandler, DomainCommandHandler.AddPolicyCommandHandler addPolicyCommandHandler)
        {
            this.getRiskByUidQueryHandler = getRiskByUidQueryHandler;
            this.addPolicyCommandHandler = addPolicyCommandHandler;
        }

        public async Task Handle(AddPolicyCommand message, IMessageHandlerContext context)
        {
            RiskByUidResult riskByUidResult = await getRiskByUidQueryHandler.HandleAsync(new GetRiskByUidQuery(message.RiskUid)).ConfigureAwait(false);

            await addPolicyCommandHandler.HandleAsync(
                new Application.Command.Commands.AddPolicyCommand(
                    message.PolicyUid,
                    riskByUidResult.RiskDto.Id,
                    message.Username,
                    riskByUidResult.RiskDto.AddressLine1,
                    riskByUidResult.RiskDto.AddressLine2,
                    riskByUidResult.RiskDto.Model,
                    riskByUidResult.RiskDto.FirstName,
                    riskByUidResult.RiskDto.LastName,
                    riskByUidResult.RiskDto.AddressUid,
                    riskByUidResult.RiskDto.Registration,
                    riskByUidResult.RiskDto.CoverType,
                    riskByUidResult.RiskDto.Make,
                    message.InsurerName,
                    message.Addons)).ConfigureAwait(false);

            await context.Publish(
                new PolicyAddedEvent
                {
                    CorrelationId = message.CorrelationId,
                }).ConfigureAwait(false);
        }
    }
}