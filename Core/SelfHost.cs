namespace Core
{
    using System;
    using Microsoft.Owin.Hosting;

    class SelfHost
    {
        static void Main(string[] args)
        {
            Console.Title = "Self-Hosted Nancy";

            string baseAddress = "http://localhost:4446/";

            // Start OWIN host 
            using (WebApp.Start<CoreStartup>(url: baseAddress))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.WriteLine("NancyFx is up and running @ " + baseAddress.ToUpper());
                Console.ReadLine();
            }
        }
    }
}
