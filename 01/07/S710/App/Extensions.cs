using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace App
{
    internal static class Extensions
    {
        public static bool IsDictionary(this Type type)
            => type.IsGenericType && typeof(IDictionary).IsAssignableFrom(type) && type.GetGenericArguments().Length == 2;
        public static bool IsCollection(this Type type)
            => typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
        public static bool IsArray(this Type type)
            => typeof(Array).IsAssignableFrom(type);
    }
}
