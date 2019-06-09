using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Vy
{
    public class FrameworkConstruction
    {
        protected IServiceCollection mServices;

        public IServiceProvider Provider { get; protected set; }
        
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

        public IFrameworkEnvironment Environment { get; protected set; }

        public IConfiguration Configuration { get; protected set; }

        public FrameworkConstruction(bool createServicecollection = true)
        {
            Environment = new DefaultFrameworkEnvironment();

            if (createServicecollection)
                Services = new ServiceCollection();
        }

        public void Build(IServiceProvider provider = null)
        {
            Provider = provider ?? Services.BuildServiceProvider();
        }

        public FrameworkConstruction UseConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;

            return this;
        }

    }
}
