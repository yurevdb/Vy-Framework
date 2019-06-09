using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;

namespace Vy
{
    public static class FrameworkExtensions
    {
        /// <summary>
        /// Adds a default logger to the <see cref="Framework"/>
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddDefaultLogger(this FrameworkConstruction construction)
        {
            construction.Services.AddLogging(options =>
            {
                options.AddConsole();
                options.AddDebug();
            });

            construction.Services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("Vy"));

            // Return construction for chaining
            return construction;
        }

        #region Configuration

        /// <summary>
        /// Adds a default configuration
        /// </summary>
        /// <param name="construction"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddDefaultConfiguration(this FrameworkConstruction construction, Action<IConfigurationBuilder> configure = null)
        {
            // Create our configuration sources
            var configurationBuilder = new ConfigurationBuilder();

            // Set base path for Json files as the startup location of the application
            configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            // Add application settings json files
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // Let custom configuration happen
            configure?.Invoke(configurationBuilder);

            // Inject configuration into services
            var configuration = configurationBuilder.Build();
            construction.Services.AddSingleton<IConfiguration>(configuration);

            // Use the configuration
            construction.UseConfiguration(configuration);

            // Return framework for chaining
            return construction;
        }

        #endregion
    }
}
