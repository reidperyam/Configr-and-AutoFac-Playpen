namespace Core
{
    using Autofac;
    using DependencyInjection;
    using Microsoft.Owin.Extensions;
    using Nancy.Owin;
    using Owin;

    public class CoreStartup
    {
        private ILifetimeScope _lifetimeScope;

        public CoreStartup(ILifetimeScope lifetimeScope)
        {
            if (lifetimeScope != null) // if inheriting ...
                _lifetimeScope = lifetimeScope;
            else // if self-hosted...
                _lifetimeScope = new RegisterTypes().ForCore(); 
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(new NancyOptions() { Bootstrapper = new CustomBootstrapper(_lifetimeScope) });
            app.UseStageMarker(PipelineStage.MapHandler); 
        }
    }
}
