using PostcodesApi.App;
using CCG.Notification.Web.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Web.PollyPolicies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.RateLimiting;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(config =>
        {
            config.Filters.Add(new ApiExceptionFilterAttribute());
        }).AddNewtonsoftJson(opts =>
        {
            // convert all enums to strings
            opts.SerializerSettings.Converters.Add(new StringEnumConverter());
            opts.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        });
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddOpenApiDocument(configure =>
        {
            configure.Title = "Postcode Api";
        });

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = 429;
            options.GlobalLimiter = PartitionedRateLimiter.CreateChained(
                PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 10,
                            QueueLimit = 0,
                            Window = TimeSpan.FromMinutes(1)
                        })),
                PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                        factory: partition => new FixedWindowRateLimiterOptions
                        {
                            AutoReplenishment = true,
                            PermitLimit = 1000,
                            QueueLimit = 0,
                            Window = TimeSpan.FromMinutes(60)
                        })));
        });

        // App
        builder.Services.AddAppServices();

        // Web
        builder.Services.AddPostcodeApiHttpClient(builder.Configuration);

        var app = builder.Build();

        app.UseOpenApi();
        app.UseSwaggerUi3();

        app.UseRateLimiter();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}