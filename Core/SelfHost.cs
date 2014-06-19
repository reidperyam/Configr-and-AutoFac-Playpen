namespace Core
{
    using System;
    using Microsoft.Owin.Hosting;
    using Common.Logging;
    using Common.Logging.Simple;

    public class SelfHost
    {
        static void Main(string[] args)
        {
            Console.Title = "Self-Hosted Nancy";
            string baseAddress = "http://localhost:4446/";
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter(LogLevel.Info, false, true, true, null);

            // Start OWIN host 
            using (WebApp.Start(url: baseAddress, startup: new CoreStartup().Configuration))
            {
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("NancyFx is up and running @ " + baseAddress.ToUpper());
                Console.ReadLine();
            }
        }
    }
}
