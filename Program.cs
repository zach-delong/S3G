using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var service = new ServiceCollection();
                service.AddCustomServices();

                var serviceProvider = service.BuildServiceProvider();

                serviceProvider.GetService<StaticSiteGenerator>().Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception was thrown");
                Console.Write(ex);
            }
        }

    }
}
