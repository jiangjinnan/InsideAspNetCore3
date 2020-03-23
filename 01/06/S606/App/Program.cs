using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            ChangeToken.OnChange(() => config.GetReloadToken(), () =>
            {
                var options = config.GetSection("format").Get<FormatOptions>();
                var dateTime = options.DateTime;
                var currencyDecimal = options.CurrencyDecimal;

                Console.WriteLine("DateTime:");
                Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
                Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
                Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
                Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

                Console.WriteLine("CurrencyDecimal:");
                Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
                Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}\n\n");
            });
            Console.Read();
        }
    }

}