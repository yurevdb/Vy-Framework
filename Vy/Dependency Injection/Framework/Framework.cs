using System;
using Microsoft.Extensions.DependencyInjection;

namespace Vy
{
    /// <summary>
    /// The Dependency Injection entry point for the application
    /// </summary>
    public static class Framework
    {
        /// <summary>
        /// Gets the base <see cref="FrameworkConstruction"/> for the <see cref="Framework"/>
        /// </summary>
        public static FrameworkConstruction Construction { get; private set; }
        
        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> of the <see cref="Framework"/>
        /// </summary>
        public static IServiceProvider Provider => Construction.Provider;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static FrameworkConstruction Construct<T>()
            where T : FrameworkConstruction, new()
        {
            Construction = new T();
            
            return Construction;
        }
        
        /// <summary>
        /// parameterized constructor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="constructionInstance"></param>
        /// <returns></returns>
        public static FrameworkConstruction Construct<T>(T constructionInstance)
            where T : FrameworkConstruction
        {
            Construction = constructionInstance;
            
            return Construction;
        }

        /// <summary>
        /// Get the specified service
        /// </summary>
        /// <typeparam name="T">The service to get</typeparam>
        /// <returns></returns>
        public static T Service<T>() => Provider.GetService<T>();
    }
}
