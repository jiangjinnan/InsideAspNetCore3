using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            
            var configuration = new ConfigurationBuilder()
                .AddExtendedXmlFile("appsettings.xml")
                .Build();

            var profiles = new Profile[]
            {
                new Profile(Gender.Male,18,"foo@outlook.com","123"),
                new Profile(Gender.Male,25,"bar@outlook.com","456"),
                new Profile(Gender.Female,36,"baz@outlook.com","789"),
            };

            var collection = configuration.Get<IEnumerable<Profile>>();
            Debug.Assert(collection.Any(it => it.Equals(profiles[0])));
            Debug.Assert(collection.Any(it => it.Equals(profiles[1])));
            Debug.Assert(collection.Any(it => it.Equals(profiles[2])));
        }
    }
}