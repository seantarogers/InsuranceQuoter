namespace InsuranceQuoter.Presentation.Api
{
    using IdentityServer4.AccessTokenValidation;
    using InsuranceQuoter.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Providers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddCors(options => { options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); });

            services.AddSingleton<CosmosClientManager>();

            var cosmosConfigurationProvider = new CosmosConfigurationProvider(Program.ApplicationSettings.CosmosEndpoint, Program.ApplicationSettings.CosmosMasterKey);
            services.AddSingleton(cosmosConfigurationProvider);

            services.AddScoped<IAsyncQueryHandler<GetAddressesByPostCodeQuery, AddressesByPostcodeResult>, GetAddressesByPostcodeQueryHandler>();
            services.AddScoped<IAsyncQueryHandler<GetCarByRegistrationNumberQuery, CarByRegistrationNumberResult>, GetCarByRegistrationNumberQueryHandler>();
            services.AddScoped<IAsyncQueryHandler<GetPoliciesByUserNameQuery, PoliciesByUserNameResult>, GetPoliciesByUserNameQueryHandler>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).AddIdentityServerAuthentication(
                options =>
                {
                    options.Authority = Program.ApplicationSettings.IdentityProviderEndpoint;
                    options.ApiName = "insurancequoterapi";
                });

            AuthorizationPolicy requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            services.AddControllers(c => c.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("Open"); //TODO would lock down in prod
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapHealthChecks("/health");
                    endpoints.MapControllers();
                });
        }
    }
}