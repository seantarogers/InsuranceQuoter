namespace InsuranceQuoter.Domain.Service.MessageHandlers
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Dtos;
    using InsuranceQuoter.Infrastructure.Message.Events;
    using NServiceBus;

    public class AddRiskCommandHandler : IHandleMessages<AddRiskCommand>
    {
        private readonly CosmosClientManager cosmosClientManager;

        public AddRiskCommandHandler(CosmosClientManager cosmosClientManager)
        {
            this.cosmosClientManager = cosmosClientManager;
        }

        public async Task Handle(AddRiskCommand message, IMessageHandlerContext context)
        {
            // We would normally revalidate on the server to protect data consistency before submitting

            var riskReference = Guid.NewGuid();

            const string RiskDocumentType = "Risk";

            await cosmosClientManager.CreateItemAsync(
                new RiskDto
                {
                    Id = riskReference,
                    AddressLine1 = message.AddressLine1,
                    AddressLine2 = message.AddressLine2,
                    City = message.City,
                    County = message.County,
                    Postcode = message.Postcode,
                    Model = message.Model,
                    Mileage = message.Mileage,
                    Fuel = message.Fuel,
                    Make = message.Make,
                    Transmission = message.Transmission,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    DateOfBirth = message.DateOfBirth,
                    AddressUid = message.AddressUid,
                    Year = message.Year,
                    Registration = message.Registration,
                    CoverType = message.CoverType,
                    Email = message.UserName,
                    Type = RiskDocumentType
                },
                CosmosConstants.DatabaseId,
                CosmosConstants.CustomerContainer,
                message.UserName).ConfigureAwait(false);

            await context.Publish(
                new RiskAddedEvent
                {
                    RiskReference = riskReference,
                    CorrelationId = message.CorrelationId
                }).ConfigureAwait(false);
        }
    }
}