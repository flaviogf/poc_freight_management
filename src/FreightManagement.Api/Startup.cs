using FreightManagement.Api.Application;
using FreightManagement.Api.Infrastructure;
using FreightManagement.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.SqlClient;

namespace FreightManagement.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnection>(it => new SqlConnection(_configuration.GetConnectionString("Default")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IStateRepository, DapperStateRepository>();

            services.AddScoped<ICityRepository, DapperCityRepository>();

            services.AddScoped<IFreightRepository, DapperFreightRepository>();

            services.AddScoped<CreateCountryFreight>();

            services.AddScoped<CreateStateFreight>();

            services.AddScoped<CreateCitiesFreight>();

            services.AddScoped<ICreateFreightStrategyFactory, CreateFreightStrategyFactory>();

            services.AddScoped<IFreightApplication, FreightApplication>();

            services.AddControllers();

            services.AddSwaggerGen(it =>
            {
                it.EnableAnnotations();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(it =>
            {
                it.SwaggerEndpoint("/swagger/v1/swagger.json", "Freight Management");
                it.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(it => it.MapControllers());
        }
    }
}