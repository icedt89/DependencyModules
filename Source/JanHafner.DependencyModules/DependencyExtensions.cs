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
        public static IServiceCollection AddModule<TDependencyModule>(this IServiceCollection services, IConfiguration? configuration = null)
            where TDependencyModule : DependencyModule, new()
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var dependencyModule = new TDependencyModule();

            return services.AddModuleCore(dependencyModule, configuration);
        }

        public static IServiceCollection AddModule(this IServiceCollection services, DependencyModule dependencyModule, IConfiguration? configuration = null)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (dependencyModule is null)
            {
                throw new ArgumentNullException(nameof(dependencyModule));
            }

            return services.AddModuleCore(dependencyModule, configuration);
        }

        private static IServiceCollection AddModuleCore(this IServiceCollection services, DependencyModule dependencyModule, IConfiguration? configuration = null)
        {
            dependencyModule.Register(services, configuration);

            return services;
        }

        public static IServiceCollection AddModulesFromCurrentAssembly(this IServiceCollection services, IConfiguration? configuration = null, Func<Assembly, IEnumerable<Type>>? typesSelector = null)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var currentAssembly = Assembly.GetCallingAssembly();

            return services.AddModulesFromAssemblyCore(currentAssembly, configuration, typesSelector);
        }

        public static IServiceCollection AddModulesFromAssembly(this IServiceCollection services, Assembly assembly, IConfiguration? configuration = null, Func<Assembly, IEnumerable<Type>>? typesSelector = null)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return services.AddModulesFromAssemblyCore(assembly, configuration, typesSelector);
        }

        private static IServiceCollection AddModulesFromAssemblyCore(this IServiceCollection services, Assembly assembly, IConfiguration? configuration = null, Func<Assembly, IEnumerable<Type>>? typesSelector = null)
        {
            typesSelector ??= a => a.GetExportedTypes();

            var dependencyModuleTypes = typesSelector(assembly).Where(t => typeof(DependencyModule).IsAssignableFrom(t));
            foreach (var dependencyModuleType in dependencyModuleTypes)
            {
                var dependencyModule = (DependencyModule)Activator.CreateInstance(dependencyModuleType);

                dependencyModule.Register(services, configuration);
            }

            return services;
        }
    }
}
