namespace Website
{
    using Api;
    using Autofac;
    using Common;
    using ConfigR;
    using Core;
    using DependencyInjection;
    using Owin;

    public class WebsiteStartup
    {
        WebsiteConfiguration _websiteConfiguration;

        public WebsiteStartup()
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
            new ApiStartup (new RegisterTypesAndInstances().ForApi( _websiteConfiguration.ApiConfiguration,  RegistrationStrategy.INHERIT)).Configuration(app);
            new CoreStartup(new RegisterTypesAndInstances().ForCore(_websiteConfiguration.CoreConfiguration, RegistrationStrategy.INHERIT)).Configuration(app);                  
        }
    }
}
