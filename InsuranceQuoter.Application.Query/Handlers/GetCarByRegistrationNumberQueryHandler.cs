namespace InsuranceQuoter.Application.Query.Handlers
{
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Message.Dtos;

    public class GetCarByRegistrationNumberQueryHandler : IAsyncQueryHandler<GetCarByRegistrationNumberQuery, CarByRegistrationNumberResult>
    {
        private readonly CosmosClientManager cosmosClientManager;

        public GetCarByRegistrationNumberQueryHandler(CosmosClientManager cosmosClientManager)
        {
            this.cosmosClientManager = cosmosClientManager;
        }

        public async Task<CarByRegistrationNumberResult> HandleAsync(GetCarByRegistrationNumberQuery query)
        {
            var sql = $"SELECT * FROM c WHERE c.registrationNumber = '{query.RegistrationNumber}'";

            var carDto = await cosmosClientManager.GetItemAsync<CarDto>(CosmosConstants.CarContainerId, CosmosConstants.DatabaseId, sql);

            return new CarByRegistrationNumberResult(carDto);
        }
    }
}