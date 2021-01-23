using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using ThermostatDotNet.Client;

namespace ThermostatDotNet.Runner.Config
{
    /// <summary>
    /// HTTP Configuration
    /// </summary>
    public static class HttpConfig
    {
        /// <summary>
        /// Configure the HTTP communication
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureHttpServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                // Domoticz APIs
                .AddApi<IThermostatDotNetService, ThermostatDotNetService>(
                    ThermostatDotNetService.GetClientConfigurator(configuration["Hosting:DomoticzApiSelection"]),
                    configuration)
                ;
        }

        /// <summary>
        /// Add and configure an API client with retry policy
        /// </summary>
        /// <typeparam name="TIApi"></typeparam>
        /// <typeparam name="TApi"></typeparam>
        /// <param name="services"></param>
        /// <param name="configureAction"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection AddApi<TIApi, TApi>(
            this IServiceCollection services,
            Action<IServiceProvider, HttpClient> configureAction,
            IConfiguration configuration
            )
            where TApi : class, TIApi
            where TIApi : class
        {
            services
                .AddHttpClient<TIApi, TApi>()
                .ConfigureHttpClient(configureAction)
                //.AddPolicyHandler(GetRetryPolicy(configuration))
                ;
            return services;
        }
    }
}