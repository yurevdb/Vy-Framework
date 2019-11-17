using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Vy
{
    /// <summary>
    /// The base dependency injection construction based on the base .net dependency injection model
    /// </summary>
    public class FrameworkConstruction
    {
        protected IServiceCollection mServices;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/>
        /// </summary>
        public IServiceProvider Provider { get; protected set; }
        
        /// <summary>
        /// Gets or sets the <see cref="IServiceCollection"/>
        /// </summary>
        public IServiceCollection Services
        {
            get => mServices;
            set
            {
                mServices = value;

                if (mServices != null)
                    Services.AddSingleton(Environment);
            }
        }

        /// <summary>
        /// Gets the <see cref="IFrameworkEnvironment"/>
        /// </summary>
        public IFrameworkEnvironment Environment { get; protected set; }

        /// <summary>
        /// Gets the <see cref="IConfiguration"/>
        /// </summary>
        public IConfiguration Configuration { get; protected set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="createServicecollection"></param>
        public FrameworkConstruction(bool createServicecollection = true)
        {
            Environment = new DefaultFrameworkEnvironment();

            if (createServicecollection)
                Services = new ServiceCollection();
        }

        /// <summary>
        /// Build the <see cref="FrameworkConstruction"/>
        /// </summary>
        /// <param name="provider"></param>
        public void Build(IServiceProvider provider = null)
        {
            Provider = provider ?? Services.BuildServiceProvider();
        }

        /// <summary>
        /// Use a specific <see cref="IConfiguration"/> for the <see cref="FrameworkConstruction"/>
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public FrameworkConstruction UseConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;

            return this;
        }

    }
}
