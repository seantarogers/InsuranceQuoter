namespace InsuranceQuoter.Infrastructure.Functions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
            where TDto : class
        {
            Database database = client.GetDatabase(databaseId);
            Container container = client.GetContainer(database.Id, containerId);

            FeedIterator<TDto> feedIterator = container.GetItemQueryIterator<TDto>(sql);

            FeedResponse<TDto> feedResponse = await feedIterator.ReadNextAsync().ConfigureAwait(false);

            return feedResponse.ToList();
        }
    }
}