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
        Configuration _coreConfiguration;
        Configuration _apiConfiguration;

        public WebsiteStartup()
        {
#if DEBUG
            Config.Global.LoadScriptFile("Web.Debug.csx");
#else
            Config.Global.LoadScriptFile("Web.Release.csx");
#endif           
            _coreConfiguration = Config.Global.Get<Configuration>("CoreConfiguration"); // loaded from bin/Core.*.csx
            _apiConfiguration  = Config.Global.Get<Configuration>("ApiConfiguration");  // loaded from bin/Api.*.csx
        }

        public void Configuration(IAppBuilder app)
        {
            new ApiStartup( new RegisterTypesAndInstances().ForApi( _apiConfiguration,  RegistrationStrategy.INHERIT)).Configuration(app);
            new CoreStartup(new RegisterTypesAndInstances().ForCore(_coreConfiguration, RegistrationStrategy.INHERIT)).Configuration(app);                  
        }
    }
}
