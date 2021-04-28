using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JanHafner.DependencyModules.Tests
{
    public sealed class DummyDependencyModule : DependencyModule
    {
        public const string DEPENDENCY = nameof(DummyDependencyModule);

        public override void Register(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton(DEPENDENCY);
        }
    }
}
