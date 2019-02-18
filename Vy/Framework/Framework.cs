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
        /// 
        /// </summary>
        public static FrameworkConstruction Construction { get; private set; }
        
        public static IServiceProvider Provider => Construction.Provider;
        
        public static FrameworkConstruction Construct<T>()
            where T : FrameworkConstruction, new()
        {
            Construction = new T();
            
            return Construction;
        }
        
        public static FrameworkConstruction Construct<T>(T constructionInstance)
            where T : FrameworkConstruction
        {
            Construction = constructionInstance;
            
            return Construction;
        }

        public static T Service<T>() =>
            Provider.GetService<T>();
    }
}
