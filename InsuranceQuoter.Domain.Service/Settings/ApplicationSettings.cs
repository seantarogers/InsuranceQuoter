namespace InsuranceQuoter.Domain.Service.Settings
{
    public class ApplicationSettings
    {
        public string ServiceBusEndpoint { get; set; }
        public string CosmosEndpoint { get; set; }
        public string CosmosMasterKey { get; set; }
    }
}