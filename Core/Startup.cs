namespace Core
{
    using Autofac;
    using Common;
    using ConfigR;
    using DependencyInjection;
    using Microsoft.Owin.Extensions;
    using Nancy.Owin;
    using Owin;

    public class Startup
    {
        private ILifetimeScope _lifetimeScope;

        public ILifetimeScope ILifetimeScope { get { return _lifetimeScope; } }

        public Startup()
        {
#if DEBUG
            Config.Global.LoadScriptFile("Core.Debug.csx");
#else
            Config.Global.LoadScriptFile("Core.Release.csx");
#endif
            _lifetimeScope = new RegisterTypesAndInstances().ForCore(Config.Global.Get<CoreConfiguration>("CoreConfiguration"));
        }

        public Startup(ILifetimeScope lifetimeScope)
        {
             _lifetimeScope = lifetimeScope;
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(new NancyOptions() { Bootstrapper = new CustomBootstrapper(_lifetimeScope) });
            app.UseStageMarker(PipelineStage.MapHandler); 
        }
    }
}
