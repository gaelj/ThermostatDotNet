using System.Net.Security;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ThermostatDotNet.Client.Contracts;
using System.Security.Cryptography.X509Certificates;

namespace ThermostatDotNet.Client
{
    public class ThermostatDotNetService : IThermostatDotNetService
    {
        private static readonly IReadOnlyDictionary<ThermostatDotNetServiceEndpoints, string> KnownEndpoints
            = new Dictionary<ThermostatDotNetServiceEndpoints, string>() {
                {ThermostatDotNetServiceEndpoints.Production, "https://192.168.30.143/"},
                {ThermostatDotNetServiceEndpoints.Localhost, "https://localhost/"},
            };

        private readonly HttpClient _httpClient;
        private IDomoticzClient _domoticzClient = null;

        private HttpClientHandler handler;

        public ThermostatDotNetService(HttpClient httpClient)
        {
            // Create an HttpClientHandler object and set to use default credentials
            handler = new HttpClientHandler();

            // Set custom server validation callback
            handler.ServerCertificateCustomValidationCallback = ServerCertificateCustomValidation;

            //_httpClient = httpClient ?? new HttpClient(handler);
            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = httpClient.BaseAddress;
            /*Task.WaitAll(new[] {
                Task.Run(async () => await KnownActionClosureTypes.Initialize(this)),
            });*/
        }

        public IDomoticzClient Domoticz
            => _domoticzClient ??= new DomoticzClient(_httpClient) {
                BaseUrl = _httpClient.BaseAddress.ToString(),
            };

        public static Action<IServiceProvider, HttpClient> GetClientConfigurator(string serviceSelection)
            => GetClientConfigurator(Enum.Parse<ThermostatDotNetServiceEndpoints>(serviceSelection));

        public static Action<IServiceProvider, HttpClient> GetClientConfigurator(ThermostatDotNetServiceEndpoints actionServiceSelection)
            => (serviceProvider, httpClient) => httpClient.BaseAddress = new Uri(KnownEndpoints[actionServiceSelection]);

        private static bool ServerCertificateCustomValidation(HttpRequestMessage requestMessage, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslErrors)
        {
            return true;
        }
    }
}