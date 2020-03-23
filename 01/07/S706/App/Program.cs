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
            var optionsAccessor = new ServiceCollection()
                .AddOptions()
                .Configure<Profile>("foo", it =>
                {
                    it.Gender = Gender.Male;
                    it.Age = 18;
                    it.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "123",
                        EmailAddress = "foo@outlook.com"
                    };
                })
                .Configure<Profile>("bar", it =>
                {
                    it.Gender = Gender.Female;
                    it.Age = 25;
                    it.ContactInfo = new ContactInfo
                    {
                        PhoneNo = "456",
                        EmailAddress = "bar@outlook.com"
                    };
                })
                .BuildServiceProvider()
                .GetRequiredService<IOptionsSnapshot<Profile>>();

            Print(optionsAccessor.Get("foo"));
            Print(optionsAccessor.Get("bar"));

            static void Print(Profile profile)
            {
                Console.WriteLine($"Gender: {profile.Gender}");
                Console.WriteLine($"Age: {profile.Age}");
                Console.WriteLine($"Email Address: {profile.ContactInfo.EmailAddress}");
                Console.WriteLine($"Phone No: {profile.ContactInfo.PhoneNo}\n");
            };
        }
    }

}
