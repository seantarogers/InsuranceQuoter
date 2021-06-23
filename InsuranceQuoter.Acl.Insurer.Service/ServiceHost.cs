namespace InsuranceQuoter.Acl.Insurer.Service
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading.Tasks;
    using InsuranceQuoter.Acl.Insurer.Service.Settings;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Extensions;
    using Microsoft.Extensions.Configuration;
    using NServiceBus;
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
            var endpointConfiguration = new EndpointConfiguration(MessagingEndpointConstants.AclInsurerService);

            endpointConfiguration.SendFailedMessagesTo(MessagingEndpointConstants.AclInsurerService + ".Error");
            endpointConfiguration.EnableInstallers();

            TransportExtensions<AzureServiceBusTransport> transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
            transport.ConnectionString(applicationSettings.ServiceBusEndpoint);

            transport.SubscriptionNamingConvention(s => s.Replace("Infrastructure.", string.Empty));
            transport.SubscriptionRuleNamingConvention(t => t.FullName.Replace("Infrastructure.", string.Empty));

            endpointConfiguration.LimitMessageProcessingConcurrencyTo(10);
            endpointConfiguration.TimeoutManager().LimitMessageProcessingConcurrencyTo(10);

            endpointConfiguration.AddUnobtrusiveMessaging();

            return endpointConfiguration;
        }
    }
}