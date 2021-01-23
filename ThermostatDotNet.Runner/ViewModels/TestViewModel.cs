using Microsoft.AspNetCore.Components;

namespace ThermostatDotNet.Runner.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private readonly DomoticzViewModel domoticzViewModel;
        public TestViewModel(NavigationManager navigationManager,
            DomoticzViewModel domoticzViewModel)
            : base(navigationManager)
        {
            this.domoticzViewModel = domoticzViewModel;
        }
    }
}