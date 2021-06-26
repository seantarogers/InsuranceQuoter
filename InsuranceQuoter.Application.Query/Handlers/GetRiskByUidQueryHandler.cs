namespace InsuranceQuoter.Application.Query.Handlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class GetRiskByUidQueryHandler : IAsyncQueryHandler<GetRiskByUidQuery, RiskByUidResult>
    {
        private readonly CosmosClientManager cosmosClientManager;

        public GetRiskByUidQueryHandler(CosmosClientManager cosmosClientManager)
        {
            this.cosmosClientManager = cosmosClientManager;
        }

        public async Task<RiskByUidResult> HandleAsync(GetRiskByUidQuery query)
        {
            var sql = $"SELECT * FROM c WHERE c.id = '{query.Uid}' and c.type = 'Risk'";

            var riskDto = await cosmosClientManager.GetItemAsync<RiskDto>(CosmosConstants.CustomerContainerId, CosmosConstants.DatabaseId, sql);

            return new RiskByUidResult(riskDto);
        }
    }
}