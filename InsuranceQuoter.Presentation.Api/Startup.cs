namespace InsuranceQuoter.Presentation.Api
{
    using InsuranceQuoter.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Handlers.Cqs.Application.Query.Handlers;
    using InsuranceQuoter.Application.Query.Queries;
    using InsuranceQuoter.Application.Query.Results;
    using InsuranceQuoter.Infrastructure.Functions;
    using InsuranceQuoter.Infrastructure.Providers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "InsuranceQuoter.Presentation.Api", Version = "v1" }); });

            services.AddCors(options => { options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); });

            services.AddSingleton<CosmosClientManager>();

            var cosmosConfigurationProvider = new CosmosConfigurationProvider(configuration["CosmosEndpoint"], configuration["CosmosMasterKey"]);
            services.AddSingleton(cosmosConfigurationProvider);

            services.AddScoped<IAsyncQueryHandler<GetAddressesByPostCodeQuery, AddressesByPostcodeResult>, GetAddressesByPostcodeQueryHandler>();
            services.AddScoped<IAsyncQueryHandler<GetCarByRegistrationNumberQuery, CarByRegistrationNumberResult>, GetCarByRegistrationNumberQueryHandler>();
            services.AddScoped<IAsyncQueryHandler<GetPoliciesByUserNameQuery, PoliciesByUserNameResult>, GetPoliciesByUserNameQueryHandler>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuranceQuoter.Presentation.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Open");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}