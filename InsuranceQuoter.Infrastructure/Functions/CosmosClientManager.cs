namespace InsuranceQuoter.Infrastructure.Functions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Message.Dtos;
    using InsuranceQuoter.Infrastructure.Providers;
    using Microsoft.Azure.Cosmos;

    public class CosmosClientManager
    {
        private readonly CosmosClient client; // TODO IDisposable?

        public CosmosClientManager(CosmosConfigurationProvider cosmosConfigurationProvider)
        {
            client = new CosmosClient(cosmosConfigurationProvider.Endpoint, cosmosConfigurationProvider.Key);
        }

        public async Task<IEnumerable<TDto>> GetItemsAsync<TDto>(string containerId, string databaseId, string sql)
            where TDto : Dto
        {
            Database database = client.GetDatabase(databaseId);
            Container container = client.GetContainer(database.Id, containerId);

            FeedIterator<TDto> feedIterator = container.GetItemQueryIterator<TDto>(
                new QueryDefinition(sql),
                null,
                new QueryRequestOptions());

            FeedResponse<TDto> feedResponse = await feedIterator.ReadNextAsync().ConfigureAwait(false);

            return feedResponse.ToList();
        }

        public Task CreateItemAsync<TDto>(TDto dto, string databaseId, string containerId, string partitionKey)
            where TDto : Dto
        {
            Database database = client.GetDatabase(databaseId);
            Container container = client.GetContainer(database.Id, containerId);

            return container.CreateItemAsync(dto, new PartitionKey(partitionKey));
        }
    }
}