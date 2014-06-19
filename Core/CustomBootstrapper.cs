namespace Core
{
    using Autofac;
    using Nancy.Bootstrappers.Autofac;

    public class CustomBootstrapper : AutofacNancyBootstrapper
    {
        private ILifetimeScope _lifetimeScope;

        public CustomBootstrapper()
        { }

        public CustomBootstrapper(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        protected override ILifetimeScope GetApplicationContainer()
        {
            return _lifetimeScope;
        }
    }
}
