using Polly;
using Polly.Extensions.Http;
using System.Net;

namespace WebApiProject.Resilience
{
    public static class ResiliencePolicies
    {
        //for retry
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(message => message.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(30));
        }

        //for circuit braker
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBrackerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));
        }

        //for rate limit
        public static IAsyncPolicy<HttpResponseMessage> GetRateLimitPolicy()
        {
            return Policy
                .RateLimitAsync<HttpResponseMessage>(2, TimeSpan.FromSeconds(30));
        }

        //for circuit braker and retry for DB
        public static IAsyncPolicy GetDbPolicy() 
        { 
            //for retry
            var retry =  Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(2, retry => TimeSpan.FromSeconds(30));

            //for curcuit braker
            var circuit = Policy
                .Handle<Exception>()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));

            return Policy.WrapAsync(retry, circuit);
        }
    }
}
