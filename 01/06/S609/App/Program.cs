using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var source = new Dictionary<string, string>
            {
                ["gender"] = "Male",
                ["age"] = "18",
                ["contactInfo:emailAddress"] = "foobar@outlook.com",
                ["contactInfo:phoneNo"] = "123456789"
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
                .Build();

            var profile = configuration.Get<Profile>();
            Debug.Assert(profile.Equals(new Profile(Gender.Male, 18, "foobar@outlook.com", "123456789")));
        }
    }

}