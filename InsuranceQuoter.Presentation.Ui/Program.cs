namespace InsuranceQuoter.Presentation.Ui
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Fluxor;
    using Fluxor.Persist.Middleware;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<SignalRConnectionManager>();
            builder.Services.AddScoped<TimerManager>();

            //builder.Services.AddSingleton<IObjectStateStorage, SessionStateStorage>();
            //builder.Services.AddSingleton<IStoreHandler, DictionaryStoreHandler>();
            //builder.Services.AddBlazoredSessionStorage();
            
            //builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/"));
            
            builder.Services.AddFluxor(
                config => config
                    .ScanAssemblies(typeof(Program).Assembly)
                    //.UsePersist()
                    .UseReduxDevTools());
            
            await builder.Build().RunAsync();

        }
    }
}