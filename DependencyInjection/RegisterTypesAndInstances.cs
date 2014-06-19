namespace DependencyInjection
{
    using Autofac;
    using Common;
    using Utilities;
    using Utilities.Contract;

    public enum RegistrationStrategy : uint { DISCRETE=0, INHERIT, REUSE, GLOBAL }

    public class RegisterTypesAndInstances
    {
        static IContainer       _globalLifetimeScope;
        static ContainerBuilder _globalContainerBuilder;

        // create a static container of global types and instance registrations (RegistrationStrategy.GLOBAL)
        private static ILifetimeScope GlobalContainer()
        {
            if (_globalLifetimeScope == null)
            {
                _globalContainerBuilder = new ContainerBuilder();

                // todo - type and instance registrations for static, global consumption...

                _globalLifetimeScope = _globalContainerBuilder.Build();
            }
            return _globalLifetimeScope;
        }

        // create a copied ContainerBuilder of shared type and instance registrations (RegistrationStrategy.INHERIT)
        private ContainerBuilder CopySharedContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            // todo - type and instance registrations for multiple consumers       
   
            return containerBuilder;
        }

        public ILifetimeScope ForApi(Configuration configuration, RegistrationStrategy registrationStrategy = RegistrationStrategy.DISCRETE, ILifetimeScope sharedLifetimeScope = null)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ApiInfo>().As<IInfo>().WithParameter("iinfo", new Info(configuration.Info, configuration.AuthenticationEnabled)).InstancePerLifetimeScope();
            return CreateContainer(containerBuilder, registrationStrategy, sharedLifetimeScope);
        }

        public ILifetimeScope ForCore(Configuration configuration, RegistrationStrategy registrationStrategy = RegistrationStrategy.DISCRETE, ILifetimeScope sharedLifetimeScope = null)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CoreInfo>().As<IInfo>().WithParameter("iinfo", new Info(configuration.Info, configuration.AuthenticationEnabled)).InstancePerLifetimeScope();
            return CreateContainer(containerBuilder, registrationStrategy, sharedLifetimeScope);
        }

        public ILifetimeScope CreateContainer(ContainerBuilder containerBuilder, RegistrationStrategy registrationStrategy, ILifetimeScope sharedLifetimeScope)
        {
            switch(registrationStrategy)
            {
                default /*Discrete*/: return containerBuilder.Build(); // create a new container with the component registrations that have been made

                case RegistrationStrategy.INHERIT: // merge containerBuilder with a copy of shared registrations and return it
                                                   ILifetimeScope inheritedLifetimeScope = containerBuilder.Build();
                                                   CopySharedContainer().Update(inheritedLifetimeScope.ComponentRegistry);
                                                   return inheritedLifetimeScope;

                case RegistrationStrategy.REUSE: // merge containerBuilder with argument container; return for subsequent consumers to do the same
                                                 containerBuilder.Update(sharedLifetimeScope.ComponentRegistry);
                                                 return sharedLifetimeScope;

                case RegistrationStrategy.GLOBAL: // mergecontainerBuilder with the global container and return it
                                                  ILifetimeScope globalLifetimeScope = GlobalContainer();
                                                  containerBuilder.Update(globalLifetimeScope.ComponentRegistry);
                                                  return globalLifetimeScope;
            }
        }
    }
}
