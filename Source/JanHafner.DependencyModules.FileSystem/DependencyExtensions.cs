using System;
using System.IO.Abstractions;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JanHafner.DependencyModules.FileSystem
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddModulesFromPath(this IServiceCollection services, string path, IFileSystem fileSystem, IConfiguration configuration = null, string searchPattern = "*.dll")
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace", nameof(path));
            }

            if (fileSystem is null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }

            var libraryFiles = fileSystem.Directory.EnumerateFiles(path, searchPattern);
            foreach (var libraryFile in libraryFiles)
            {
                var assembly = Assembly.LoadFile(libraryFile);

                services.AddModulesFromAssembly(assembly, configuration);
            }

            return services;
        }
    }
}
