using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JanHafner.DependencyModules.Tests.DependencyExtensionsTests
{
    public sealed class AddModule
    {
        [Fact]
        public void ThrowsArgumentNullExceptionIfServiceCollectionIsNullViaGenericType()
        {
            // Arrange
            IServiceCollection serviceCollection = null;
            IConfiguration configuration = null;

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => serviceCollection.AddModule<DummyDependencyModule>(configuration));
        }

        [Fact]
        public void RegistersDependenciesViaGenericTypeCorrectly()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            IConfiguration configuration = null;

            // Act
            serviceCollection.AddModule<DummyDependencyModule>(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Assert
            var dependency = serviceProvider.GetRequiredService<string>();
            dependency.Should().Be(DummyDependencyModule.DEPENDENCY);
        }

        [Fact]
        public void ThrowsArgumentNullExceptionIfServiceCollectionIsNullViaDependencyModuleInstance()
        {
            // Arrange
            IServiceCollection serviceCollection = null;
            IConfiguration configuration = null;
            var dependencyModule = new DummyDependencyModule();

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => serviceCollection.AddModule(dependencyModule, configuration));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionIfDependencyModuleIsNullViaDependencyModuleInstance()
        {
            // Arrange
            var serviceCollectionMock = new Mock<IServiceCollection>();
            IConfiguration configuration = null;
            DependencyModule dependencyModule = null;

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => serviceCollectionMock.Object.AddModule(dependencyModule, configuration));
        }

        [Fact]
        public void RegistersDependenciesViaDependencyModuleInstanceCorrectly()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            IConfiguration configuration = null;

            // Act
            serviceCollection.AddModule(new DummyDependencyModule(), configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Assert
            var dependency = serviceProvider.GetRequiredService<string>();
            dependency.Should().Be(DummyDependencyModule.DEPENDENCY);
        }
    }
}
