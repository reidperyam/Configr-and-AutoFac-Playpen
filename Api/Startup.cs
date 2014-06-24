namespace Api
{
    using Autofac;
    using DependencyInjection;
    using Owin;
    using System.Web.Http;
    using Common;
    using ConfigR;

    public class Startup
    {
        private ILifetimeScope _lifetimeScope;

        public ILifetimeScope ILifetimeScope { get { return _lifetimeScope; } }

        public Startup()
        {
#if DEBUG
            Config.Global.LoadScriptFile("Api.Debug.csx");
#else
            Config.Global.LoadScriptFile("Api.Release.csx");
#endif
            _lifetimeScope = new RegisterTypesAndInstances().ForApi(Config.Global.Get<ApiConfiguration>("ApiConfiguration"));
        }

        public Startup(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();      
            WebApiConfiguration.Configure(config, _lifetimeScope);
            app.UseWebApi(config);
        }
    }
}
