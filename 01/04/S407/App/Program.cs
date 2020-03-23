using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {

            BuildServiceProvider(false);
            BuildServiceProvider(true);

            static void BuildServiceProvider(bool validateOnBuild)
            {
                try
                {
                    var options = new ServiceProviderOptions
                    {
                        ValidateOnBuild = validateOnBuild
                    };
                    new ServiceCollection()
                        .AddSingleton<IFoobar, Foobar>()
                        .BuildServiceProvider(options);
                    Console.WriteLine($"Status: Success; ValidateOnBuild: {validateOnBuild}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Status: Fail; ValidateOnBuild: {validateOnBuild}");
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
