using System.Diagnostics;
using System.Reflection;

namespace Vy
{
    /// <summary>
    /// Environment variables useful throughout the application
    /// </summary>
    public class DefaultFrameworkEnvironment : IFrameworkEnvironment
    {
        /// <summary>
        /// Gets in wich configuration the application is running
        /// </summary>
        public string Configuration => IsDevelopment ? "Development" : "Production";

        /// <summary>
        /// Get wether the application is running in <see cref="Debug"/> mode or not
        /// </summary>
        public bool IsDevelopment => Assembly.GetEntryAssembly()?.GetCustomAttribute<DebuggableAttribute>()?.IsJITTrackingEnabled == true;

        public DefaultFrameworkEnvironment() { }
    }
}
