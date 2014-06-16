namespace DependencyInjection
{
    using Autofac;
    using Common;
    using Configuration;
    using Utilities;
    using Utilities.Contract;

    public class RegisterTypes
    {
        private readonly CommonConfiguration _configuration;

        public RegisterTypes()
        {
            _configuration = new Configurator().GetConfiguration();
        }

        public ILifetimeScope ForApi()
        {          
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ApiInfo>().As<IInfo>().WithParameter("iinfo",new Info(_configuration.Info)).InstancePerLifetimeScope();
            return containerBuilder.Build();
        }

        public ILifetimeScope ForCore()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CoreInfo>().As<IInfo>().WithParameter("iinfo", new Info(_configuration.Info)).InstancePerLifetimeScope();
            return containerBuilder.Build();
        }

        public ILifetimeScope ForWebsite()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<WebsiteInfo>().As<IInfo>().WithParameter("iinfo", new Info(_configuration.Info)).InstancePerLifetimeScope();
            return containerBuilder.Build();
        }
    }
}
