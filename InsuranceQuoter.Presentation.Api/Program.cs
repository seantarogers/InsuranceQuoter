namespace InsuranceQuoter.Presentation.Api
{
    using System.IO;
    using InsuranceQuoter.Presentation.Api.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public class Program
    {
        internal static ApplicationSettings ApplicationSettings { get; private set; }

        public static void Main(string[] args)
        {
            ApplicationSettings = DeserializeAppSettings();

            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder.UseUrls(ApplicationSettings.PresentationApiEndpoint);
                        webBuilder.UseStartup<Startup>();
                    });

        private static ApplicationSettings DeserializeAppSettings() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()
                .Get<ApplicationSettings>();
    }
}