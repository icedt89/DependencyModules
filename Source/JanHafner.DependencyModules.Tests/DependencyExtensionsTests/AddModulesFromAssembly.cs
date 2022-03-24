using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Reflection;
using Xunit;

namespace JanHafner.DependencyModules.Tests.DependencyExtensionsTests;

public sealed class AddModulesFromAssembly
{
    [Fact]
    public void ThrowsArgumentNullExceptionIfServiceCollectionIsNull()
    {
        // Arrange
        IServiceCollection? serviceCollection = null;
        var assembly = Assembly.GetCallingAssembly();
        IConfiguration? configuration = null;

        // Act, Assert
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentNullException>(() => serviceCollection.AddModulesFromAssembly(assembly, configuration));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Fact]
    public void ThrowsArgumentNullExceptionIfAssemblyIsNull()
    {
        // Arrange
        var serviceCollectionMock = new Mock<IServiceCollection>();
        Assembly? assembly = null;
        IConfiguration? configuration = null;

        // Act, Assert
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.Throws<ArgumentNullException>(() => serviceCollectionMock.Object.AddModulesFromAssembly(assembly, configuration));
#pragma warning restore CS8604 // Possible null reference argument.
    }

    [Fact]
    public void RegistersDependenciesCorrectly()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();
        IConfiguration? configuration = null;

        // Act
        serviceCollection.AddModulesFromAssembly(assembly, configuration);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        var dependency = serviceProvider.GetRequiredService<string>();
        dependency.Should().Be(DummyDependencyModule.DEPENDENCY);
    }

    [Fact]
    public void ThrowsExceptionIfDependencyModuleHasNoPublicConstructor()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();
        IConfiguration? configuration = null;

        // Act, Assert
        Assert.ThrowsAny<Exception>(() => serviceCollection.AddModulesFromAssembly(assembly, configuration, a => new[] { typeof(DummyDependencyModuleWithoutPublicConstructor) }));
    }

    [Fact]
    public void ThrowsExceptionIfDependencyModuleHasNoPublicParameterlessConstructor()
    {
        // Arrange
        var serviceCollection = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();
        IConfiguration? configuration = null;

        // Act, Assert
        Assert.ThrowsAny<Exception>(() => serviceCollection.AddModulesFromAssembly(assembly, configuration, a => new[] { typeof(DummyDependencyModuleWithoutPublicParameterlessConstructor) }));
    }

    [Fact]
    public void RegistersDependenciesOfExportedTypesIfTypeSelectorIsNullCorrectly()
    {
        var serviceCollection = new ServiceCollection();
        var assembly = Assembly.GetExecutingAssembly();
        IConfiguration? configuration = null;

        // Act
        serviceCollection.AddModulesFromAssembly(assembly, configuration, null);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Assert
        var dependency = serviceProvider.GetRequiredService<string>();
        dependency.Should().Be(DummyDependencyModule.DEPENDENCY);
    }
}
