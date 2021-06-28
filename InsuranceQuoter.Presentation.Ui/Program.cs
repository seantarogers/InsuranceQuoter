namespace InsuranceQuoter.Presentation.Ui
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Fluxor;
    using InsuranceQuoter.Presentation.Ui.Exceptions;
    using InsuranceQuoter.Presentation.Ui.Functions;
    using InsuranceQuoter.Presentation.Ui.Providers;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault();

            string presentationApiEndpoint = GetPresentationApiEndpoint(builder);
            string identityProviderEndpoint = Get(builder);
            string hubEndpoint = GetHubEndpoint(builder);
            string presentationUiEndpoint = GetPresentationUiEndpoint(builder);

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<SignalRConnectionManager>();
            builder.Services.AddScoped<TimerManager>();
            builder.Services.AddSingleton(new EndpointProvider(presentationApiEndpoint, hubEndpoint));
            builder.Services.AddTransient<AccessTokenExtractor>();

            builder.Services.AddOidcAuthentication(
                options =>
                {
                    options.ProviderOptions.Authority = identityProviderEndpoint;
                    options.ProviderOptions.ClientId = "insurancequoter";
                    options.ProviderOptions.RedirectUri = $"{presentationUiEndpoint}/authentication/login-callback";
                    options.ProviderOptions.PostLogoutRedirectUri = $"{presentationUiEndpoint}/authentication/logout-callback";
                    options.ProviderOptions.DefaultScopes.Add("email");
                    options.ProviderOptions.DefaultScopes.Add("profile");
                    options.ProviderOptions.DefaultScopes.Add("email");
                    options.ProviderOptions.DefaultScopes.Add("insurancequoterapi");
                    options.ProviderOptions.ResponseType = "code";
                });

            //TODO add redux state into session storage rather than in js memory
            //builder.Services.AddSingleton<IObjectStateStorage, SessionStateStorage>();
            //builder.Services.AddSingleton<IStoreHandler, DictionaryStoreHandler>();
            //builder.Services.AddBlazoredSessionStorage();
            //builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("https://localhost:44340/"));

            builder.Services.AddFluxor(
                config => config
                    .ScanAssemblies(typeof(Program).Assembly)
                    //.UsePersist() //TODO wire up Session Storage
                    .UseReduxDevTools());

            await builder.Build().RunAsync();
        }

        private static string GetPresentationUiEndpoint(WebAssemblyHostBuilder builder)
        {
            string presentationUiEndpoint = builder.Configuration["PresentationUiEndpoint"];
            if (string.IsNullOrWhiteSpace(presentationUiEndpoint))
            {
                throw new AppSettingsItemNotFoundException();
            }

            return presentationUiEndpoint;
        }

        private static string GetHubEndpoint(WebAssemblyHostBuilder builder)
        {
            string hubEndpoint = builder.Configuration["HubEndpoint"];
            if (string.IsNullOrWhiteSpace(hubEndpoint))
            {
                throw new AppSettingsItemNotFoundException();
            }

            return hubEndpoint;
        }

        private static string Get(WebAssemblyHostBuilder builder)
        {
            string identityProviderEndpoint = builder.Configuration["IdentityProviderEndpoint"];
            if (string.IsNullOrWhiteSpace(identityProviderEndpoint))
            {
                throw new AppSettingsItemNotFoundException();
            }

            return identityProviderEndpoint;
        }

        private static string GetPresentationApiEndpoint(WebAssemblyHostBuilder builder)
        {
            string presentationApiEndpoint = builder.Configuration["PresentationApiEndpoint"];
            if (string.IsNullOrWhiteSpace(presentationApiEndpoint))
            {
                throw new AppSettingsItemNotFoundException();
            }

            return presentationApiEndpoint;
        }
    }
}