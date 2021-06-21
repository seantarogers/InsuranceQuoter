namespace InsuranceQuoter.Saga.Service
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using NServiceBus;
    using NServiceBus.Faults;
    using Topshelf;

    [ExcludeFromCodeCoverage]
    public class ServiceHost : IServiceHost
    {
        private static HostControl hostControl;
        private static IEndpointInstance endpointInstance;

        public bool Start(HostControl topshelfHostControl = null)
        {
            hostControl = topshelfHostControl;

            try
            {
                EndpointConfiguration endpointConfiguration = BuildEndpointConfiguration();

                endpointInstance = StartEndpoint(endpointConfiguration);

                Console.WriteLine("Service started successfully");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Cannot start service. Details: ${exception}");

                return false;
            }

            return true;
        }

        public bool Stop()
        {
            try
            {
                Task.Run(async () => await endpointInstance.Stop()).GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Cannot start service. Details: ${exception}");
                return false;
            }

            return true;
        }

        private static IEndpointInstance StartEndpoint(EndpointConfiguration configuration)
        {
            return Task.Run(async () => await Endpoint.Start(configuration).ConfigureAwait(false)).GetAwaiter()
                .GetResult();
        }

        private static EndpointConfiguration BuildEndpointConfiguration()
        {
            var endpointConfiguration = new EndpointConfiguration("InsuranceQuoter.Saga.Service");

            endpointConfiguration.SendFailedMessagesTo("InsuranceQuoter.Saga.Service" + ".Error");
            //endpointConfiguration.AuditProcessedMessagesTo(EndpointNameConstants.QuoteLeadInfrastructureAuditService);
            endpointConfiguration.EnableInstallers();

            TransportExtensions<AzureServiceBusTransport> transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString("Endpoint=sb://insurancequoter.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=83n6GvgGScyg/KF2dCTcLETUBiKXtL4Jp0kkzNOVzOU=");
            ConventionsBuilder conventions = endpointConfiguration.Conventions();
            conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Events"));
            conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains(".Commands"));
            conventions.DefiningMessagesAs(t => t.Namespace != null && (t.Namespace.Contains(".Requests") || t.Namespace.Contains(".Responses") || t.Namespace.Contains(".Timeouts")));

            return endpointConfiguration;
        }

        private static Task OnMessageSentToErrorQueue(FailedMessage failedMessage) =>
            //applicationLogger.ErrorFormat(EndpointNameConstants.QuoteLeadDomainService, $"Moving failed message to the {EndpointNameConstants.QuoteLeadDomainService}.Error queue. Exception: {failedMessage.Exception}.");
            Task.CompletedTask;
    }
}