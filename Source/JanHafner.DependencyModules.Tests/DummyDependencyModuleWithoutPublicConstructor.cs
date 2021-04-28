using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JanHafner.DependencyModules.Tests
{
    internal sealed class DummyDependencyModuleWithoutPublicConstructor : DependencyModule
    {
        public const string DEPENDENCY = nameof(DummyDependencyModuleWithoutPublicConstructor);

        private DummyDependencyModuleWithoutPublicConstructor()
        {
        }

        public override void Register(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton(DEPENDENCY);
        }
    }
}
