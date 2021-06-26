namespace InsuranceQuoter.Application.Query.Handlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class GetAddressesByPostcodeQueryHandler : IAsyncQueryHandler<GetAddressesByPostCodeQuery, AddressesByPostcodeResult>
    {
        private readonly CosmosClientManager cosmosClientManager;

        public GetAddressesByPostcodeQueryHandler(CosmosClientManager cosmosClientManager)
        {
            this.cosmosClientManager = cosmosClientManager;
        }

        public async Task<AddressesByPostcodeResult> HandleAsync(GetAddressesByPostCodeQuery query)
        {
            var sql = $"SELECT * FROM c WHERE c.postcode = '{query.Postcode}'";

            IEnumerable<AddressDto> addressDtos = await cosmosClientManager.GetItemsAsync<AddressDto>(CosmosConstants.AddressContainerId, CosmosConstants.DatabaseId, sql);

            return new AddressesByPostcodeResult(addressDtos);
        }
    }
}