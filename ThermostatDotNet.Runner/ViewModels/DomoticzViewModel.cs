using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ThermostatDotNet.Client;
using ThermostatDotNet.Client.Contracts;
using ThermostatDotNet.Runner.Helpers;

namespace ThermostatDotNet.Runner.ViewModels
{
    public class DomoticzViewModel : BaseStaticViewModel
    {
        private readonly IThermostatDotNetService thermostatDotNetService;

        public DomoticzViewModel(IThermostatDotNetService thermostatDotNetService)
        {
            this.thermostatDotNetService = thermostatDotNetService;
        }

        public async Task RetrieveDomoticzDeviceStatusList()
        {
            try {
                var ret = await thermostatDotNetService.Domoticz.QueryAsync(Client.Contracts.Type.Devices, filter: Filter.All, used: true, order: Order.Name);
                DomoticzDeviceStatusList = ret.ConvertAs<DomoticzDeviceStatusList>();
                NotifyPropertyChanged(nameof(DomoticzDeviceStatusList));
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                DomoticzDeviceStatusList = null;
            }
        }

        public DomoticzInstanceStatus DomoticzInstanceStatus { get; set; }
        public DomoticzDeviceStatusList DomoticzDeviceStatusList { get; set; }
    }
}