using ThermostatDotNet.Client.Contracts;

namespace ThermostatDotNet.Runner.Helpers
{
    public static class DomoticzQueryHelper
    {
        public static T ConvertAs<T>(this Response response) where T : new()
        {
            var r = new T();
            foreach (var p in typeof(T).GetProperties()) {
                var val = typeof(Response).GetProperty(p.Name).GetValue(response);
                p.SetValue(r, val);
            }
            return r;
        }
    }
}