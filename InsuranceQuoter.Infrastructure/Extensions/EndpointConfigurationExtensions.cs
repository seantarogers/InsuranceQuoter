namespace InsuranceQuoter.Infrastructure.Extensions
{
    using NServiceBus;

    public static class EndpointConfigurationExtensions
    {
        public static EndpointConfiguration AddUnobtrusiveMessaging(this EndpointConfiguration endpointConfiguration)
        {
            ConventionsBuilder conventions = endpointConfiguration.Conventions();

            conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Events"));
            conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains(".Commands"));
            conventions.DefiningMessagesAs(t => t.Namespace != null && (t.Namespace.Contains(".Requests") || t.Namespace.Contains(".Responses")));

            return endpointConfiguration;
        }
    }
}