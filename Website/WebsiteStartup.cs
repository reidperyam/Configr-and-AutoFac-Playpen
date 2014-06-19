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

        ILifetimeScope _sharedLifetimeScope;
        ILifetimeScope _coreLifetimeScope;
        ILifetimeScope _apiLifetimeScope;

        public WebsiteStartup()
        {
#if DEBUG
            Config.Global.LoadScriptFile("Web.Debug.csx");
#else
            Config.Global.LoadScriptFile("Web.Release.csx");
#endif           
            _coreConfiguration = Config.Global.Get<Configuration>("CoreConfiguration"); // loaded from bin/Core.*.csx
            _apiConfiguration  = Config.Global.Get<Configuration>("ApiConfiguration");   // loaded from bin/Api.*.csx

            //_discreteLifetimeScope = Config.Global.Get<bool>("DiscreteLifetimeScope");
        }

        public void Configuration(IAppBuilder app)
        {
            //if (_discreteLifetimeScope)
            //{
            //    // create separate DI containers relating to specific, loaded .csx configurations 
            //_coreLifetimeScope = new RegisterTypes().ForCoreOnly(_coreConfiguration);
            //_apiLifetimeScope = new RegisterTypes().ForApiOnly(_apiConfiguration);

            //    new ApiStartup(_apiLifetimeScope).Configuration(app);
            //    new CoreStartup(_coreLifetimeScope).Configuration(app);
            //}
            //else
            //{
                // allow a shared DI container between components of the Website
                _sharedLifetimeScope = new RegisterTypes().Shared();

                //_coreLifetimeScope = new RegisterTypes().ForCore(_coreConfiguration);
                //_apiLifetimeScope = new RegisterTypes().ForApi(_apiConfiguration);

                new RegisterTypes().ForCoreInherit(_coreConfiguration, _sharedLifetimeScope);
                new RegisterTypes().ForApiInherit(_apiConfiguration,   _sharedLifetimeScope);


                //_coreLifetimeScope = new RegisterTypes().ForCoreDaisyChain(_coreConfiguration, _sharedLifetimeScope);
                //_apiLifetimeScope = new RegisterTypes().ForCoreDaisyChain(_apiConfiguration, _sharedLifetimeScope);

                new ApiStartup(_apiLifetimeScope).Configuration(app);
                new CoreStartup(_coreLifetimeScope).Configuration(app);
            //}                    
        }
    }
}
