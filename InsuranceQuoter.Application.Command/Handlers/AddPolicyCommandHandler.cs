namespace InsuranceQuoter.Application.Command.Handlers
{
    using System;
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Command.Commands;
    using InsuranceQuoter.Application.Command.Results;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class AddPolicyCommandHandler : IAsyncCommandHandler<AddPolicyCommand, AddPolicyResult>
    {
        private readonly CosmosClientManager cosmosClientManager;

        public AddPolicyCommandHandler(CosmosClientManager cosmosClientManager)
        {
            this.cosmosClientManager = cosmosClientManager;
        }

        public AddPolicyResult Result { get; private set; }

        public async Task HandleAsync(AddPolicyCommand command)
        {
            // Would normally validate the incoming command

            const string PolicyDocumentType = "Policy";

            DateTime policyStartsOn = DateTime.UtcNow;
            DateTime policyExpiresOn = DateTime.UtcNow.AddYears(1);

            await cosmosClientManager.CreateItemAsync(
                new PolicyDto
                {
                    Id = command.PolicyUid,
                    AddressLine1 = command.AddressLine1,
                    Postcode = command.Postcode,
                    Model = command.Model,
                    Make = command.Make,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    AddressUid = command.AddressUid,
                    Registration = command.Registration,
                    CoverType = command.CoverType,
                    Email = command.Email,
                    Type = PolicyDocumentType,
                    StartsOn = policyStartsOn,
                    ExpiresOn = policyExpiresOn,
                    InsurerName = command.InsurerName,
                    AddOns = command.Addons
                },
                CosmosConstants.DatabaseId,
                CosmosConstants.CustomerContainerId,
                command.Email).ConfigureAwait(false);

            Result = new AddPolicyResult(command.PolicyUid);
        }
    }
}