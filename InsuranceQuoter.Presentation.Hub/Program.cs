namespace InsuranceQuoter.Presentation.Hub
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using InsuranceQuoter.Infrastructure.Constants;
    using InsuranceQuoter.Infrastructure.Extensions;
    using InsuranceQuoter.Infrastructure.Message.Commands;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using InsuranceQuoter.Presentation.Hub.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using NServiceBus;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        public static void Main(string[] args)
        {
            var applicationSettings = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .Get<ApplicationSettings>();

            BuildWebHost(args, applicationSettings).Build().Run();
        }

        public static IHostBuilder BuildWebHost(string[] args, ApplicationSettings applicationSettings) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    c =>
                        c.UseStartup<Startup>()
                            .UseUrls(applicationSettings.HubEndpoint)
                )
                .UseNServiceBus(
                    _ =>
                    {
                        var endpointConfiguration = new EndpointConfiguration(MessagingEndpointConstants.PresentationHub);

                        endpointConfiguration.SendFailedMessagesTo(MessagingEndpointConstants.PresentationHub + ".Error");
                        endpointConfiguration.EnableInstallers();
                        endpointConfiguration.ConfigureAzureServiceBusTransport(applicationSettings.ServiceBusEndpoint);
                        endpointConfiguration.AddUnobtrusiveMessaging();
                        endpointConfiguration.LimitMessageProcessingConcurrencyTo(10);
                        endpointConfiguration.TimeoutManager().LimitMessageProcessingConcurrencyTo(10);

                        TransportExtensions<AzureServiceBusTransport> transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();

                        transport.Routing().RouteToEndpoint(
                            typeof(QuoteRequest),
                            MessagingEndpointConstants.SagaService
                        );

                        transport.Routing().RouteToEndpoint(
                            typeof(TakePaymentCommand),
                            MessagingEndpointConstants.SagaService);

                        return endpointConfiguration;
                    });
    }
}