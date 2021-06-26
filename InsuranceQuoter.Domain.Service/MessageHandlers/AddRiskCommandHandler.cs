namespace InsuranceQuoter.Domain.Service.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Command.Handlers;
    using InsuranceQuoter.Application.Command.Results;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using NServiceBus;
    using DomainCommands = InsuranceQuoter.Application.Command.Commands;

    public class AddRiskCommandHandler : IHandleMessages<AddRiskCommand>
    {
        private readonly IAsyncCommandHandler<DomainCommands.AddRiskCommand, AddRiskResult> addRiskCommandHandler;

        public AddRiskCommandHandler(IAsyncCommandHandler<DomainCommands.AddRiskCommand, AddRiskResult> addRiskCommandHandler)
        {
            this.addRiskCommandHandler = addRiskCommandHandler;
        }

        public async Task Handle(AddRiskCommand message, IMessageHandlerContext context)
        {
            // We would normally revalidate on the server to protect data consistency before submitting

            await addRiskCommandHandler.HandleAsync(
                new DomainCommands.AddRiskCommand(
                    message.AddressLine1,
                    message.AddressLine2,
                    message.City,
                    message.County,
                    message.Postcode,
                    message.Model,
                    message.Transmission,
                    message.FirstName,
                    message.LastName,
                    message.DateOfBirth,
                    message.AddressUid,
                    message.Year,
                    message.Registration,
                    message.CoverType,
                    Email: message.UserName,
                    message.Mileage,
                    message.Fuel,
                    message.Make)).ConfigureAwait(false);

            Guid riskUid = addRiskCommandHandler.Result.RiskUid;

            await context.Publish(
                new RiskAddedEvent
                {
                    RiskUid = riskUid,
                    CorrelationId = message.CorrelationId
                }).ConfigureAwait(false);
        }
    }
}