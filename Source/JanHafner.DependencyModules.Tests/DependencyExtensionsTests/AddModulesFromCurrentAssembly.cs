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
    public sealed class AddModulesFromCurrentAssembly
    {
        [Fact]
        public void ThrowsArgumentNullExceptionIfServiceCollectionIsNull()
        {
            // Arrange
            IServiceCollection serviceCollection = null;
            IConfiguration configuration = null;

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => serviceCollection.AddModulesFromCurrentAssembly(configuration));
        }

        [Fact]
        public void RegistersDependenciesCorrectly()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            IConfiguration configuration = null;

            // Act
            serviceCollection.AddModulesFromCurrentAssembly(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Assert
            var dependency = serviceProvider.GetRequiredService<string>();
            dependency.Should().Be(DummyDependencyModule.DEPENDENCY);
        }
    }
}
