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
            var source = new Dictionary<string, string>
            {
                ["foo:gender"] = "Male",
                ["foo:age"] = "18",
                ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
                ["foo:contactInfo:phoneNo"] = "123",

                ["bar:gender"] = "Male",
                ["bar:age"] = "25",
                ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
                ["bar:contactInfo:phoneNo"] = "456",

                ["baz:gender"] = "Female",
                ["baz:age"] = "36",
                ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
                ["baz:contactInfo:phoneNo"] = "789"
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(source)
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

            var array = configuration.Get<Profile[]>();
            Debug.Assert(array[0].Equals(profiles[1]));
            Debug.Assert(array[1].Equals(profiles[2]));
            Debug.Assert(array[2].Equals(profiles[0]));

        }
    }

}