namespace InsuranceQuoter.Infrastructure.Providers
{
    public class CosmosConfigurationProvider
    {
        public CosmosConfigurationProvider(string endpoint, string key)
        {
            Endpoint = endpoint;
            Key = key;
        }

        public string Endpoint { get; }
        public string Key { get; }
    }
}