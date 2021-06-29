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
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Configuration;
    using NServiceBus;
    using Topshelf;

    [ExcludeFromCodeCoverage]
    public class ServiceHost : IServiceHost
    {
        private static IEndpointInstance endpointInstance;

        public bool Start(HostControl topshelfHostControl = null)
        {
            ApplicationSettings applicationSettings = DeserializeApplicationSettings();

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

        private static ApplicationSettings DeserializeApplicationSettings()
        {
            var applicationSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .Get<ApplicationSettings>();
            return applicationSettings;
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

            endpointConfiguration.UsePersistence<CosmosPersistence>()
                .CosmosClient(new CosmosClient(applicationSettings.CosmosEndpoint))
                .DatabaseName(applicationSettings.CosmosDatabaseName)
                .DefaultContainer(
                    containerName: "Saga",
                    partitionKeyPath: "/id");

            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();

            //OutboxSettings outbox = endpointConfiguration.EnableOutbox();
            //outbox.TimeToKeepOutboxDeduplicationData(TimeSpan.FromDays(7));

            //PipelineSettings pipeline = endpointConfiguration.Pipeline;
            //pipeline.Register(
            //    behavior: new CosmosOutboxBehavior(),
            //    description: "Joins the read from the Service Bus queue to the write to the Cosmos database ");

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

            transport.Routing().RouteToEndpoint(
                typeof(TakeCardPaymentCommand),
                MessagingEndpointConstants.AclPaymentProviderService
            );

            transport.Routing().RouteToEndpoint(
                typeof(BindPolicyWithInsurerCommand),
                MessagingEndpointConstants.AclInsurerService
            );

            transport.Routing().RouteToEndpoint(
                typeof(AddPolicyCommand),
                MessagingEndpointConstants.DomainService
            );

            return endpointConfiguration;
        }
    }
}