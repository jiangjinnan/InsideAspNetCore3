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
            ["foo"] = null,
            ["bar"] = "",
            ["baz"] = "123"
        };

        var root = new ConfigurationBuilder()
            .AddInMemoryCollection(source)
            .Build();

        //针对object
        Debug.Assert(root.GetValue<object>("foo") == null);
        Debug.Assert("".Equals(root.GetValue<object>("bar")));
        Debug.Assert("123".Equals(root.GetValue<object>("baz")));

        //针对普通类型
        Debug.Assert(root.GetValue<int>("foo") == 0);
        Debug.Assert(root.GetValue<int>("baz") == 123);

        //针对Nullable<T>
        Debug.Assert(root.GetValue<int?>("foo") == null);
        Debug.Assert(root.GetValue<int?>("bar") == null);

    }
}
}