using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JanHafner.DependencyModules.Tests;

internal sealed class DummyDependencyModuleWithoutPublicParameterlessConstructor : DependencyModule
{
    public const string DEPENDENCY = nameof(DummyDependencyModuleWithoutPublicParameterlessConstructor);

    public DummyDependencyModuleWithoutPublicParameterlessConstructor(string test)
    {
    }

    public override void Register(IServiceCollection services, IConfiguration? configuration = null)
    {
        services.AddSingleton(DEPENDENCY);
    }
}
