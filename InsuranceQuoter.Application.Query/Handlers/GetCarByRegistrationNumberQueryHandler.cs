namespace InsuranceQuoter.Application.Query.Handlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Dtos;
    using InsuranceQuoter.Infrastructure.Functions;

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

            IEnumerable<CarDto> carDtos = (await cosmosClientManager.GetItemsAsync<CarDto>(CosmosConstants.CarContainerId, CosmosConstants.DatabaseId, sql)).ToList();

            if (!carDtos.Any())
            {
                return new CarByRegistrationNumberResult(default);
            }

            CarDto matchingCar = carDtos.Single();

            return new CarByRegistrationNumberResult(matchingCar);
        }
    }
}