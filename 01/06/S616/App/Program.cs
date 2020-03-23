using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace App
{
    public class Program
    {
        static void Main()
        {
            var initialSettings = new Dictionary<string, string>
            {
                ["Gender"] = "Male",
                ["Age"] = "18",
                ["ContactInfo:EmailAddress"] = "foobar@outlook.com",
                ["ContactInfo:PhoneNo"] = "123456789"
            };

            var profile = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .AddDatabase("DefaultDb", initialSettings)
                .Build()
                .Get<Profile>();

            Debug.Assert(profile.Gender == Gender.Male);
            Debug.Assert(profile.Age == 18);
            Debug.Assert(profile.ContactInfo.EmailAddress == "foobar@outlook.com");
            Debug.Assert(profile.ContactInfo.PhoneNo == "123456789");
        }
    }

}