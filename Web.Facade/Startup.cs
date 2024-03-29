﻿// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Web.Facade
{
    using System;
    using System.Reflection;
    using Authorization.Service.Extentions;
    using CloudStorage.Service.Extentions;
    using Infrastructure.Database.Extentions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Serilog;
    using Serilog.Exceptions;
    using Serilog.Sinks.Elasticsearch;
    using Users.Service.Extentions;
    using Web.Facade.Middlewares;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseServices(this.Configuration);
            services.AddAuthServices(this.Configuration);
            services.AddUserServices(this.Configuration);
            services.AddCloudStorageServices(this.Configuration);

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorization API", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        Array.Empty<string>()
                    },
                });
            });

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(this.Configuration["ElasticConfiguration:Uri"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                    NumberOfReplicas = 1,
                    NumberOfShards = 2,
                })
                .Enrich.WithProperty("Enviroment", environment)
                .ReadFrom.Configuration(this.Configuration)
                .CreateLogger();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var supportedCultures = new[] { "en-US", "ru-RU" };
            var localizationOptions =
                new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseMiddleware<HttpRequestBodyMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
