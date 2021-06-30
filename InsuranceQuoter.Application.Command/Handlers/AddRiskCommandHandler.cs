namespace InsuranceQuoter.Application.Command.Handlers
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Command.Commands;
    using InsuranceQuoter.Application.Command.Results;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class AddRiskCommandHandler : IAsyncCommandHandler<AddRiskCommand, AddRiskResult>
    {
        private readonly CosmosClientManager cosmosClientManager;

        public AddRiskCommandHandler(CosmosClientManager cosmosClientManager)
        {
            this.cosmosClientManager = cosmosClientManager;
        }

        public AddRiskResult Result { get; private set; }

        public async Task HandleAsync(AddRiskCommand command)
        {
            // Would normally validate the incoming command

            var riskUid = Guid.NewGuid();

            const string RiskDocumentType = "Risk";

            await cosmosClientManager.CreateItemAsync(
                new RiskDto
                {
                    Id = riskUid,
                    AddressLine1 = command.AddressLine1,
                    AddressLine2 = command.AddressLine2,
                    City = command.City,
                    County = command.County,
                    Postcode = command.Postcode,
                    Model = command.Model,
                    Mileage = command.Mileage,
                    Fuel = command.Fuel,
                    Make = command.Make,
                    Transmission = command.Transmission,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    DateOfBirth = command.DateOfBirth,
                    AddressUid = command.AddressUid,
                    Year = command.Year,
                    Registration = command.Registration,
                    CoverType = command.CoverType,
                    Email = command.Email,
                    Type = RiskDocumentType
                },
                CosmosConstants.DatabaseId,
                CosmosConstants.CustomerContainerId,
                command.Email).ConfigureAwait(false);

            Result = new AddRiskResult(riskUid);
        }
    }
}