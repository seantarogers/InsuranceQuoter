namespace InsuranceQuoter.Presentation.Ui
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Providers;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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
            builder.Services.AddSingleton(new HostNameProvider("https://localhost:44307"));
            builder.Services.AddTransient<AccessTokenExtractor>();

            builder.Services.AddOidcAuthentication(
                options =>
                {
                    options.ProviderOptions.Authority = "https://localhost:5000";
                    options.ProviderOptions.ClientId = "insurancequoter";
                    options.ProviderOptions.RedirectUri = "https://localhost:5001/authentication/login-callback";
                    options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:5001/authentication/logout-callback";
                    options.ProviderOptions.DefaultScopes.Add("email");
                    options.ProviderOptions.DefaultScopes.Add("profile");
                    options.ProviderOptions.DefaultScopes.Add("email");
                    options.ProviderOptions.DefaultScopes.Add("insurancequoterpresentationapi");
                    options.ProviderOptions.ResponseType = "code";
                });

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