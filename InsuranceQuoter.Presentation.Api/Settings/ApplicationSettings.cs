namespace InsuranceQuoter.Presentation.Api.Settings
{
    public class ApplicationSettings
    {
        public string CosmosEndpoint { get; set; }
        public string CosmosMasterKey { get; set; }
        public string IdentityProviderEndpoint { get; set; }
        public string PresentationApiEndpoint { get; set; }
    }
}