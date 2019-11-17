using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vy
{
    /// <summary>
    /// 
    /// </summary>
    public class FrameworkDI
    {
        /// <summary>
        /// Gets the configuration
        /// </summary>
        public static IConfiguration Configuration => Framework.Provider.GetService<IConfiguration>();
    }
}
