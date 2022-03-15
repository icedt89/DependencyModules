using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace JanHafner.DependencyModules.Tests.DependencyExtensionsTests
{
    public sealed class AddModule
    {
        [Fact]
        public void ThrowsArgumentNullExceptionIfServiceCollectionIsNull()
        {
            // Arrange
            IServiceCollection? serviceCollection = null;
            IConfiguration? configuration = null;
            var dependencyModule = new DummyDependencyModule();

            // Act, Assert
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(() => serviceCollection.AddModule(dependencyModule, configuration));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void ThrowsArgumentNullExceptionIfDependencyModuleIsNull()
        {
            // Arrange
            var serviceCollectionMock = new Mock<IServiceCollection>();
            IConfiguration? configuration = null;
            DependencyModule? dependencyModule = null;

            // Act, Assert
#pragma warning disable CS8604 // Possible null reference argument.
            Assert.Throws<ArgumentNullException>(() => serviceCollectionMock.Object.AddModule(dependencyModule, configuration));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Fact]
        public void RegistersDependenciesCorrectly()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            IConfiguration? configuration = null;

            // Act
            serviceCollection.AddModule(new DummyDependencyModule(), configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Assert
            var dependency = serviceProvider.GetRequiredService<string>();
            dependency.Should().Be(DummyDependencyModule.DEPENDENCY);
        }
    }
}
