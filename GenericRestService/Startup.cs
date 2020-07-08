using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Lamar;
using AutoMapper;
using ILogger = Serilog.ILogger;
using Serilog;
using Serilog.Core;
using Serilog.Configuration;
using Swashbuckle.AspNetCore;

using Data;
using GenericRestService.ControllerFactory;
using Data.Repositories;
using Data.entities;

namespace GenericRestService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ServiceRegistry serviceRegistry)
        {
            
            var logger = new LoggerConfiguration()
               .ReadFrom.Configuration(Configuration)
               .CreateLogger();
            logger.Information("Logger has been configured.");
            serviceRegistry.Scan(s =>
            {
                s.TheCallingAssembly();
                s.AssembliesFromApplicationBaseDirectory((assembly) => { return assembly.FullName.Contains("kyc"); });
                s.AssemblyContainingType<IAlbumsDBContext>();
                s.WithDefaultConventions();
            });
            serviceRegistry.For<ILogger>().Use(logger);
            var mapperConfig = new MapperConfiguration(
                    mc => mc.AddProfile(new MapperProfile())
                );
            serviceRegistry.AddSingleton(mapperConfig.CreateMapper());
            serviceRegistry.AddControllers();
            serviceRegistry.AddDbContext<AlbumsDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AlbumsDBContext")), ServiceLifetime.Transient);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IAlbumsDBContext), typeof(IAlbumsDBContext));
            services.AddScoped(typeof(ILogger), typeof(Logger));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddDbContext<AlbumsDBContext>();
            services.AddScoped<ILogger, Logger>();
            services.AddControllers();
            var mvcBuilder = services.AddMvc();
            mvcBuilder.AddMvcOptions(o => o.Conventions.Add(new GenericRestControllerNameConvention()));
            mvcBuilder.ConfigureApplicationPartManager(c =>
            {
                c.FeatureProviders.Add(new GenericRestControllerFeatureProvider());
            });
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Appointment API");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}