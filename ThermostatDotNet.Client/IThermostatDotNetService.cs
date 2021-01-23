using ThermostatDotNet.Client.Contracts;

namespace ThermostatDotNet.Client
{
    public interface IThermostatDotNetService
    {
        IDomoticzClient Domoticz { get; }
    }
}