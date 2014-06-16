namespace Configuration
{
    using Common;
    using ConfigR;
    using Common.Logging;
    using Common.Logging.Simple;

    public class Configurator
    {
        public CommonConfiguration GetConfiguration()
        {
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter(LogLevel.All, false, true, true, null);

#if DEBUG
            Config.Global.LoadScriptFile("devConfig.csx");
#else
            Config.Global.LoadScriptFile("prodConfig.csx");
#endif
            return Config.Global.Get<CommonConfiguration>("Configuration");
        }
    }
}
