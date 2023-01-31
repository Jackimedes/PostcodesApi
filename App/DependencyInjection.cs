using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PostcodesApi.App.Services;
using PostcodesApi.App.Services.Contracts;
using System.Reflection;

namespace PostcodesApi.App
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services) 
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IPostcodesIoApi, PostcodesIoApi>();

            return services;
        }
    }
}