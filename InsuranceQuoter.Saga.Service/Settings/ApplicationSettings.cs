namespace InsuranceQuoter.Saga.Service.Settings
{
    public class ApplicationSettings
    {
        public string ServiceBusEndpoint { get; set; }
        public string CosmosEndpoint { get; set; }
        public string CosmosDatabaseName { get; set; }
    }
}