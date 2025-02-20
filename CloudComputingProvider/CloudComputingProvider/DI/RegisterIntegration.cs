using CloudComputingProvider.Helpers;
using CloudComputingProvider.Services.Interfaces.Mock;
using Refit;

namespace CloudComputingProvider.DI
{
    public static class RegisterIntegration
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the custom HTTP message handler
            services.AddTransient<MockHttpMessageHandler>();

            // Create an HttpClient using the custom message handler
            services.AddHttpClient("MockedHttpClient")
                    .ConfigurePrimaryHttpMessageHandler<MockHttpMessageHandler>();

            // Register your Refit API client
            services.AddRefitClient<IMockCcpApi>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration.GetSection("ApiPaths")["MockCcpApi"]))
                    .AddHttpMessageHandler<MockHttpMessageHandler>();

            //// Register your service that uses HttpClient
            //services.AddTransient<IYourService, YourService>();

        }
    }
}
