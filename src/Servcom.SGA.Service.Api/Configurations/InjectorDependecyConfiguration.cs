using Microsoft.Extensions.DependencyInjection;
using Servcom.SGA.Infra.CrossCutting.IoC.InjectorDependecies;

namespace Servcom.SGA.Service.Api.Configurations
{
    public static class InjectorDependecyConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services)
        {

            InjectorDependecy.RegisterServices(services);
        }
    }
}
