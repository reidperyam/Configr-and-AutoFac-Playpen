namespace Api
{
    using Autofac;
    using Autofac.Integration.WebApi;
    using System.Web.Http;
    using Common;

    public class WebApiConfiguration
    {
        public static void Configure(HttpConfiguration config, ILifetimeScope lifetimeScope)
        {
            ConfigureWebApi(config);
            ConfigureAutofac(config, lifetimeScope);
            config.EnsureInitialized();
        }

        private static void ConfigureWebApi(HttpConfiguration config)
        {
            config.EnableSystemDiagnosticsTracing();
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.EnableCors();
           config.MapHttpAttributeRoutes();
        }

        public static void ConfigureAutofac(HttpConfiguration config, ILifetimeScope lifetimeScope)
        {
            // load the upstream configuration if needed
            // CommonConfiguration commonConfiguration = lifetimeScope.Resolve<CommonConfiguration>();

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(WebApiConfiguration).Assembly);
            builder.RegisterHttpRequestMessage(config);
            builder.Update(lifetimeScope.ComponentRegistry);//update the argument LifetimeScope with the registered types from this scope 
            config.DependencyResolver = new AutofacWebApiDependencyResolver(lifetimeScope);
        }
    }
}
