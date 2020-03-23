using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace App
{
public class Program
{
    public static void Main()
    {
        Environment.SetEnvironmentVariable("TEST_GENDER", "Male");
        Environment.SetEnvironmentVariable("TEST_AGE", "18");
        Environment.SetEnvironmentVariable("TEST_CONTACTINFO:EMAILADDRESS", "foobar@outlook.com");
        Environment.SetEnvironmentVariable("TEST_CONTACTINFO:PHONENO", "123456789");

        var profile = new ConfigurationBuilder()
            .AddEnvironmentVariables("TEST_")
            .Build()
            .Get<Profile>();

        Debug.Assert(profile.Equals(new Profile(Gender.Male, 18, "foobar@outlook.com", "123456789")));
    }
}
}