namespace Api
{
    using Microsoft.Owin.Hosting;
    using System;
    using Common.Logging;
    using Common.Logging.Simple;

    public class SelfHost
    {
        static void Main(string[] args)
        {
            Console.Title = "Self-Hosted Api";
            string baseAddress = "http://localhost:4445/";
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter(LogLevel.Info, false, true, true, null);

            // Start OWIN host 
            using (WebApp.Start(url: baseAddress, startup: new ApiStartup().Configuration))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.WriteLine("ASP.NET Web API is up and running @ " + baseAddress.ToUpper());
                Console.ReadLine();
            }
        }
    }
}
