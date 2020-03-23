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
                ["foo:gender"] = "男",
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

            var collection = configuration.Get<IEnumerable<Profile>>();
            Debug.Assert(collection.Count() == 2);

            var array = configuration.Get<Profile[]>();
            Debug.Assert(array.Length == 3);
            Debug.Assert(array[2] == null);
            //由于配置节按照Key进行排序，绑定失败的配置节为最后一个
        }
    }

}