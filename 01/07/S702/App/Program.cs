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
                .AddJsonFile("profile.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("foo", configuration.GetSection("foo"))
                .Configure<Profile>("bar", configuration.GetSection("bar"))
                .BuildServiceProvider();

            var optionsAccessor = serviceProvider
                .GetRequiredService<IOptionsSnapshot<Profile>>();
            Print(optionsAccessor.Get("foo"));
            Print(optionsAccessor.Get("bar"));

            static void Print(Profile profile)
            {
                Console.WriteLine($"Gender: {profile.Gender}");
                Console.WriteLine($"Age: {profile.Age}");
                Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
                Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
            }
        }
    }
}
