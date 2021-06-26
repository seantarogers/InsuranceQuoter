namespace InsuranceQuoter.Acl.PaymentProvider.Service
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading.Tasks;
    using InsuranceQuoter.Acl.PaymentProvider.Service.Settings;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Extensions;
    using Microsoft.Extensions.Configuration;
    using NServiceBus;
    using Topshelf;

    [ExcludeFromCodeCoverage]
    public class ServiceHost : IServiceHost
    {
        private static IEndpointInstance endpointInstance;

        public bool Start(HostControl topshelfHostControl = null)
        {
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
            var endpointConfiguration = new EndpointConfiguration(MessagingEndpointConstants.AclPaymentProviderService);

            endpointConfiguration.SendFailedMessagesTo(MessagingEndpointConstants.AclPaymentProviderService + ".Error");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.ConfigureAzureServiceBusTransport(applicationSettings.ServiceBusEndpoint);
            endpointConfiguration.AddUnobtrusiveMessaging();

            return endpointConfiguration;
        }
    }
}