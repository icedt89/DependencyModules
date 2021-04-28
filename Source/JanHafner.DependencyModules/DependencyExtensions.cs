using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JanHafner.DependencyModules
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddModule<TDependencyModule>(this IServiceCollection serviceCollection, IConfiguration configuration = null)
            where TDependencyModule : DependencyModule, new()
        {
            if (serviceCollection is null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            var dependencyModule = new TDependencyModule();

            return serviceCollection.AddModule(dependencyModule, configuration);
        }

        public static IServiceCollection AddModule(this IServiceCollection serviceCollection, DependencyModule dependencyModule, IConfiguration configuration = null)
        {
            if (serviceCollection is null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (dependencyModule is null)
            {
                throw new ArgumentNullException(nameof(dependencyModule));
            }

            dependencyModule.Register(serviceCollection, configuration);

            return serviceCollection;
        }

        public static IServiceCollection AddModulesFromCurrentAssembly(this IServiceCollection serviceCollection, IConfiguration configuration = null, Func<Assembly, IEnumerable<Type>> typesSelector = null)
        {
            if (serviceCollection is null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            var currentAssembly = Assembly.GetCallingAssembly();

            return serviceCollection.AddModulesFromAssembly(currentAssembly, configuration, typesSelector);
        }

        public static IServiceCollection AddModulesFromAssembly(this IServiceCollection serviceCollection, Assembly assembly, IConfiguration configuration = null, Func<Assembly, IEnumerable<Type>> typesSelector = null)
        {
            if (serviceCollection is null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            typesSelector ??= a => a.GetExportedTypes();

            var dependencyModuleTypes = typesSelector(assembly).Where(t => typeof(DependencyModule).IsAssignableFrom(t));
            foreach (var dependencyModuleType in dependencyModuleTypes)
            {
                var dependencyModule = (DependencyModule)Activator.CreateInstance(dependencyModuleType);

                dependencyModule.Register(serviceCollection, configuration);
            }

            return serviceCollection;
        }
    }
}
