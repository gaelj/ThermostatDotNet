using Microsoft.Extensions.DependencyInjection;
using ThermostatDotNet.Runner.ViewModels;

namespace ThermostatDotNet.Runner.Config
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services)
            => services
                //.AddTransient<MsgHelper>()
                //.AddTransient<ClipboardHelper>()
                ;

        public static IServiceCollection AddViewModels(this IServiceCollection services)
            => services
                .AddScoped<TestViewModel>()
                .AddSingleton<DomoticzViewModel>()
                ;
    }
}