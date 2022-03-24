using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace JanHafner.DependencyModules.Tests.DependencyExtensionsTests;

public sealed class AddModulesFromCurrentAssembly
{
    [Fact]
    public void ThrowsArgumentNullExceptionIfServiceCollectionIsNull()
    {
        // Arrange
        IServiceCollection? serviceCollection = null;
        IConfiguration? configuration = null;

        // Act, Assert
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentNullException>(() => serviceCollection.AddModulesFromCurrentAssembly(configuration));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Fact]
    public void RegistersDependenciesCorrectly()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        IConfiguration? configuration = null;

        // Act
        serviceCollection.AddModulesFromCurrentAssembly(configuration);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        var dependency = serviceProvider.GetRequiredService<string>();
        dependency.Should().Be(DummyDependencyModule.DEPENDENCY);
    }
}
