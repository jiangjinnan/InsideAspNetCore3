using Microsoft.Extensions.Configuration;
using System;

namespace App
{
    public class Program
    {
public static void Main()
{
    var format = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build()
        .GetSection("format")
        .Get<FormatOptions>();


    var dateTime = format.DateTime;
    var currencyDecimal = format.CurrencyDecimal;

    Console.WriteLine("DateTime:");
    Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
    Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
    Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
    Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

    Console.WriteLine("CurrencyDecimal:");
    Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
    Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");
}
    }
}