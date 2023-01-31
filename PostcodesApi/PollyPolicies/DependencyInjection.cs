using Web.PollyPolicies.Policies;

namespace Web.PollyPolicies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPostcodeApiHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            string? url = configuration["ExternalApi:PostcodeIo:BaseUrl"];
            if (string.IsNullOrWhiteSpace(url)) 
                throw new ArgumentNullException(nameof(url));

            services.AddHttpClient<PostcodesApi.App.Services.Contracts.IPostcodesIoApi, PostcodesApi.App.Services.PostcodesIoApi>(client =>
            {
                client.BaseAddress = new Uri(url);
            })
            .AddPolicyHandler(HttpClientPolicies.GetRetryPolicy());

            return services;
        }
    }
}
