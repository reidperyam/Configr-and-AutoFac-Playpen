namespace Api
{
    using Microsoft.Owin.Hosting;
    using System;

    class SelfHost
    {
        static void Main(string[] args)
        {
            Console.Title = "Self-Hosted Api";

            string baseAddress = "http://localhost:4445/";

            // Start OWIN host 
            using (WebApp.Start<ApiStartup>(url: baseAddress))
            {
                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.WriteLine("ASP.NET Web API is up and running @ " + baseAddress.ToUpper());
                Console.ReadLine();
            }
        }
    }
}
