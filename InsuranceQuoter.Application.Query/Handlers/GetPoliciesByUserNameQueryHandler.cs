namespace InsuranceQuoter.Application.Query.Handlers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class GetPoliciesByUserNameQueryHandler : IAsyncQueryHandler<GetPoliciesByUserNameQuery, PoliciesByUserNameResult>
    {
        private readonly CosmosClientManager cosmosClientManager;

        public GetPoliciesByUserNameQueryHandler(CosmosClientManager cosmosClientManager)
        {
            this.cosmosClientManager = cosmosClientManager;
        }

        public async Task<PoliciesByUserNameResult> HandleAsync(GetPoliciesByUserNameQuery query)
        {
            Thread.Sleep(500);

            var sql = $"SELECT * FROM c WHERE c.email = '{query.UserName}' AND c.type = 'Policy'";

            IEnumerable<PolicyDto> policyDtos = await cosmosClientManager.GetItemsAsync<PolicyDto>(CosmosConstants.CustomerContainerId, CosmosConstants.DatabaseId, sql);

            return new PoliciesByUserNameResult(policyDtos);
        }
    }
}