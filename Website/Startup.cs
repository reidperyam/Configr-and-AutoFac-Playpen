namespace Website
{
    using Autofac;
    using Common;
    using ConfigR;
    using DependencyInjection;
    using Owin;

    public class Startup
    {
        WebsiteConfiguration _websiteConfiguration;

        public Startup()
        {
#if DEBUG
            Config.Global.LoadScriptFile("Web.Debug.csx");
#else
            Config.Global.LoadScriptFile("Web.Release.csx");
#endif           
            _websiteConfiguration = Config.Global.Get<WebsiteConfiguration>("WebsiteConfiguration"); 
        }

        public void Configuration(IAppBuilder app)
        {
            new Api.Startup (new RegisterTypesAndInstances().ForApi( _websiteConfiguration.ApiConfiguration,  RegistrationStrategy.INHERIT)).Configuration(app);
            new Core.Startup(new RegisterTypesAndInstances().ForCore(_websiteConfiguration.CoreConfiguration, RegistrationStrategy.INHERIT)).Configuration(app);                  
        }
    }
}
