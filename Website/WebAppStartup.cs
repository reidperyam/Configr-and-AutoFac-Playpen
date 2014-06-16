namespace Website
{
    using Api;
    using Autofac;
    using Core;
    using DependencyInjection;
    using Owin;

    public class WebAppStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ILifetimeScope lifetimeScope = new RegisterTypes().ForWebsite();
            new ApiStartup(lifetimeScope).Configuration(app);
            new CoreStartup(lifetimeScope).Configuration(app);
        }
    }
}
