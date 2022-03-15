using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JanHafner.DependencyModules.Tests
{
#pragma warning disable S3453 // Classes should not have only "private" constructors
    internal sealed class DummyDependencyModuleWithoutPublicConstructor : DependencyModule
#pragma warning restore S3453 // Classes should not have only "private" constructors
    {
        public const string DEPENDENCY = nameof(DummyDependencyModuleWithoutPublicConstructor);

        private DummyDependencyModuleWithoutPublicConstructor()
        {
        }

        public override void Register(IServiceCollection services, IConfiguration? configuration = null)
        {
            services.AddSingleton(DEPENDENCY);
        }
    }
}
