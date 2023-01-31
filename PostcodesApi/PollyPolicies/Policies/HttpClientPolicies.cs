using Polly.Extensions.Http;
using Polly;

namespace Web.PollyPolicies.Policies
{
    internal static class HttpClientPolicies
    {
        internal static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }
    }
}
