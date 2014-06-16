namespace Api
{
    using Autofac;
    using DependencyInjection;
    using Owin;
    using System.Web.Http;

    public class ApiStartup
    {
        private ILifetimeScope _lifetimeScope;

        public ApiStartup(ILifetimeScope lifetimeScope)
        {
            if (lifetimeScope != null) // if inheriting ...
                _lifetimeScope = lifetimeScope;
            else // if self-hosted...
                _lifetimeScope = new RegisterTypes().ForApi(); 
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();      
            WebApiConfiguration.Configure(config, _lifetimeScope);
            app.UseWebApi(config);
        }
    }
}
