namespace InsuranceQuoter.Saga.Service
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Extensions;
    using Microsoft.Azure.Cosmos;
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
            var endpointConfiguration = new EndpointConfiguration(MessagingEndpointConstants.SagaService);

            endpointConfiguration.SendFailedMessagesTo(MessagingEndpointConstants.SagaService + ".Error");
            endpointConfiguration.EnableInstallers();

            PersistenceExtensions<CosmosPersistence> persistence = endpointConfiguration.UsePersistence<CosmosPersistence>();
            var connection = "AccountEndpoint=https://seanrogers.documents.azure.com:443/;AccountKey=5GCBm84eTfJJ9cXhWYbanIfX4bO7RKhvlauzvExFY6806Pdjd0io3xKcLUyGnuhoKMp9hB9aeHVKohvGamu8WA==";
            persistence.DatabaseName("Samples.CosmosDB.Simple");
            persistence.CosmosClient(new CosmosClient(connection));
            persistence.DefaultContainer("Server", "/id");

            TransportExtensions<AzureServiceBusTransport> transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString("Endpoint=sb://insurancequoter.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=83n6GvgGScyg/KF2dCTcLETUBiKXtL4Jp0kkzNOVzOU=");

            endpointConfiguration.AddUnobtrusiveMessaging();

            endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Sagas>();
            endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Subscriptions>();
            endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Timeouts>();
            endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Outbox>();

            return endpointConfiguration;
        }

        private static Task OnMessageSentToErrorQueue(FailedMessage failedMessage) =>
            //applicationLogger.ErrorFormat(EndpointNameConstants.QuoteLeadDomainService, $"Moving failed message to the {EndpointNameConstants.QuoteLeadDomainService}.Error queue. Exception: {failedMessage.Exception}.");
            Task.CompletedTask;
    }
}