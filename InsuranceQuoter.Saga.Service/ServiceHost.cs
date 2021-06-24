namespace InsuranceQuoter.Saga.Service
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading.Tasks;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Extensions;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Saga.Service.Settings;
    using Microsoft.Extensions.Configuration;
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

            var applicationSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .Get<ApplicationSettings>();

            try
            {
                EndpointConfiguration endpointConfiguration = BuildEndpointConfiguration(applicationSettings);

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

        private static EndpointConfiguration BuildEndpointConfiguration(ApplicationSettings applicationSettings)
        {
            var endpointConfiguration = new EndpointConfiguration(MessagingEndpointConstants.SagaService);

            endpointConfiguration.SendFailedMessagesTo(MessagingEndpointConstants.SagaService + ".Error");
            endpointConfiguration.EnableInstallers();

            endpointConfiguration.ConfigureAzureServiceBusTransport(applicationSettings.ServiceBusEndpoint);
            endpointConfiguration.AddUnobtrusiveMessaging();

            endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Sagas>();
            endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Subscriptions>();
            endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Timeouts>();
            endpointConfiguration.UsePersistence<InMemoryPersistence, StorageType.Outbox>();

            TransportExtensions<AzureServiceBusTransport> transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();

            transport.Routing().RouteToEndpoint(
                typeof(AddRiskCommand),
                MessagingEndpointConstants.DomainService
            );

            transport.Routing().RouteToEndpoint(
                typeof(AbcInsurerQuoteRequest),
                MessagingEndpointConstants.AclInsurerService
            );

            transport.Routing().RouteToEndpoint(
                typeof(DefInsurerQuoteRequest),
                MessagingEndpointConstants.AclInsurerService
            );

            transport.Routing().RouteToEndpoint(
                typeof(GhiInsurerQuoteRequest),
                MessagingEndpointConstants.AclInsurerService
            );

            transport.Routing().RouteToEndpoint(
                typeof(JklInsurerQuoteRequest),
                MessagingEndpointConstants.AclInsurerService
            );

            transport.Routing().RouteToEndpoint(
                typeof(MnoInsurerQuoteRequest),
                MessagingEndpointConstants.AclInsurerService
            );

            return endpointConfiguration;
        }

        private static Task OnMessageSentToErrorQueue(FailedMessage failedMessage) =>
            //applicationLogger.ErrorFormat(EndpointNameConstants.QuoteLeadDomainService, $"Moving failed message to the {EndpointNameConstants.QuoteLeadDomainService}.Error queue. Exception: {failedMessage.Exception}.");
            Task.CompletedTask;
    }
}