using System;
using System.Data;
using System.Linq;
using System.Reflection;
using BookShop.Configuration;
using Data.Migrations;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace BookShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddJson();
            services.AddControllers();
            services.AddRepositories();
            services.AddStorageManagers();
            services.AddDatabaseFactory(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}/BookShop.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "BookShop", Version = "v1"});
            });

            // services.AddMvc()
                // .AddJsonOptions(option => option.ConfigureMvc());
            
            services.AddFluentMigratorCore()
                .ConfigureRunner(config =>
                    config.AddPostgres()
                        .WithGlobalConnectionString(Configuration.GetConnectionString("DefaultConnection"))
                        // .ScanIn(AppDomain.CurrentDomain.GetAssemblies()
                        // .Single(assembly => assembly.GetName().Name.Equals("BookShop.Data"))
                        // ) 
                        .ScanIn(typeof(AddedBooksAndAuthors).Assembly)
                        .For.All()
                )
                .AddLogging(config => config.AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookShop v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseMvc();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            //TODO: Вынести в отдельное приложение

            #region Migrator

            using var scope = app.ApplicationServices.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator?.ListMigrations();
            migrator?.MigrateUp();

            #endregion
        }
    }
}