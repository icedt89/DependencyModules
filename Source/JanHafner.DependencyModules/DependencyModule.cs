using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JanHafner.DependencyModules
{
    public abstract class DependencyModule
    {
        public abstract void Register(IServiceCollection services, IConfiguration? configuration = null);
    }
}
