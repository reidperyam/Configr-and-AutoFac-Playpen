namespace DependencyInjection
{
    using Autofac;
    using Common;
    using Utilities;
    using Utilities.Contract;

    public class RegisterTypes
    {
        // support capability to allow a single, shared DI container between components that registers types and instances not dependant on configuration
        public ILifetimeScope Shared()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            // todo - define types and instances shared between API & Core          
            return containerBuilder.Build();
        }

        // define injected types specific to the Api
        public ILifetimeScope ForApi(Configuration configuration)
        {          
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ApiInfo>().As<IInfo>().WithParameter("iinfo", new Info(configuration.Info, configuration.AuthenticationEnabled)).InstancePerLifetimeScope();
            return containerBuilder.Build();
        }

        public void ForApiInherit(Configuration configuration, ILifetimeScope lifetimeScope)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ApiInfo>().As<IInfo>().WithParameter("iinfo", new Info(configuration.Info, configuration.AuthenticationEnabled)).InstancePerLifetimeScope();
            containerBuilder.Update(lifetimeScope.ComponentRegistry);
        }

        // define injected types specific to the Api and integrate additional shared
        public ILifetimeScope ForApiDaisyChain(Configuration configuration, ILifetimeScope lifetimeScope)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ApiInfo>().As<IInfo>().WithParameter("iinfo", new Info(configuration.Info, configuration.AuthenticationEnabled)).InstancePerLifetimeScope();
            containerBuilder.Update(lifetimeScope.ComponentRegistry);
            return lifetimeScope;
        }

        // define injected types specific to the Core
        public ILifetimeScope ForCore(Configuration configuration)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CoreInfo>().As<IInfo>().WithParameter("iinfo", new Info(configuration.Info, configuration.AuthenticationEnabled)).InstancePerLifetimeScope();
            return containerBuilder.Build();
        }

        public void ForCoreInherit(Configuration configuration, ILifetimeScope inheritedScope)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CoreInfo>().As<IInfo>().WithParameter("iinfo", new Info(configuration.Info, configuration.AuthenticationEnabled)).InstancePerLifetimeScope();
            containerBuilder.Update(inheritedScope.ComponentRegistry);
        }

        // consume shared registrations and define others to be shared through the application pipeline
        public ILifetimeScope ForCoreDaisyChain(Configuration configuration, ILifetimeScope lifetimeScope)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CoreInfo>().As<IInfo>().WithParameter("iinfo", new Info(configuration.Info, configuration.AuthenticationEnabled)).InstancePerLifetimeScope();
            containerBuilder.Update(lifetimeScope.ComponentRegistry);
            return lifetimeScope;
        }
    }
}
