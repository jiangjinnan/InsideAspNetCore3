using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "profile.json", optional: false, reloadOnChange: true)
                .Build();

            new ServiceCollection()
                .AddOptions()
                .Configure<Profile>(configuration)
                .BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<Profile>>()
                .OnChange(profile =>
                {
                    Console.WriteLine($"Gender: {profile.Gender}");
                    Console.WriteLine($"Age: {profile.Age}");
                    Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
                    Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
                });
            Console.Read();
        }
    }
}
