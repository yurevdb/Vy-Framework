using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
    }
}
