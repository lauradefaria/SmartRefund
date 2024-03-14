using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SmartRefund.Application.Interfaces;
using SmartRefund.Application.Services;
using SmartRefund.Infra.Context;
using SmartRefund.Infra.Interfaces;
using SmartRefund.Infra.Repositories;
using SmartRefund.WebAPI.Middlewares;
using System.Diagnostics.CodeAnalysis;

namespace SmartRefund.WebAPI
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var apiName = "SmartRefund Web API";
            var builder = WebApplication.CreateBuilder(args);

            // Add Logging
            builder.Services.AddLogging();
            builder.Services.AddMemoryCache();

            // Controllers
            builder.Services.AddControllers(options =>
            {
                // Custom Exception Filter
                options.Filters.Add<ExceptionFilter>();
            }
            );

            // Remove os provedores de log padr?o**
            builder.Logging.ClearProviders();

            // Adiciona os log no console**
            builder.Logging.AddConsole();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1" });
                c.EnableAnnotations();
            });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SmartRefundSqlite"));
            });

            // Add OpenAIKey EnvVar
            builder.Configuration.AddEnvironmentVariables(
                builder.Configuration.GetSection("OpenAIVisionConfig:EnvVariable").Value
                );

            // Services
            builder.Services.AddScoped<IFileValidatorService, FileValidatorService>();
            builder.Services.AddScoped<IVisionExecutorServiceConfiguration, VisionExecutorServiceConfiguration>();
            builder.Services.AddScoped<IVisionExecutorService, VisionExecutorService>();
            builder.Services.AddScoped<IVisionTranslatorService, VisionTranslatorService>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddScoped<IInternalAnalyzerService, InternalAnalyzerService>();


            // Repositories
            builder.Services.AddScoped<ITranslatedVisionReceiptRepository, TranslatedVisionReceiptRepository>();
            builder.Services.AddScoped<IRawVisionReceiptRepository, RawVisionReceiptRepository>();
            builder.Services.AddScoped<IInternalReceiptRepository, InternalReceiptRepository>();
            // Add CacheService
         

            builder.Services.AddHostedService<VisionProcessingWorker>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Custom Logging Middleware
            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}