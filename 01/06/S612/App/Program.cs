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

        var profiles = configuration.Get<IDictionary<string, Profile>>();
        Debug.Assert(profiles["foo"].Equals(new Profile(Gender.Male, 18, "foo@outlook.com", "123")));
        Debug.Assert(profiles["bar"].Equals(new Profile(Gender.Male, 25, "bar@outlook.com", "456")));
        Debug.Assert(profiles["baz"].Equals(new Profile(Gender.Female, 36, "baz@outlook.com", "789")));
    }
}
}