namespace InsuranceQuoter.Presentation.Hub
{
    using System.Diagnostics.CodeAnalysis;
    using InsuranceQuoter.Infrastructure.Message.Requests;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using NServiceBus;

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Build().Run();
        }

        public static IHostBuilder BuildWebHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    c =>
                        c.UseStartup<Startup>()
                            .UseUrls("https://localhost:9001")
                )
                .UseNServiceBus(
                    c =>
                    {
                        var endpointConfiguration = new EndpointConfiguration("InsuranceQuote.Presentation.Hub");

                        endpointConfiguration.SendFailedMessagesTo("InsuranceQuote.Presentation.Hub" + ".Error");
                        //endpointConfiguration.AuditProcessedMessagesTo(EndpointNameConstants.QuoteLeadInfrastructureAuditService);
                        endpointConfiguration.EnableInstallers();
                        TransportExtensions<AzureServiceBusTransport> transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
                        transport.ConnectionString("AnAzureServiceBusConnectionString");

                        transport.Routing().RouteToEndpoint(
                            typeof(QuotesRequest),
                            "InsuranceQuoter.Saga.Service"
                        );

                        ConventionsBuilder conventions = endpointConfiguration.Conventions();
                        conventions.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Events"));
                        conventions.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains(".Commands"));
                        conventions.DefiningMessagesAs(t => t.Namespace != null && (t.Namespace.Contains(".Requests") || t.Namespace.Contains(".Responses") || t.Namespace.Contains(".Timeouts")));

                        return endpointConfiguration;
                    });
    }