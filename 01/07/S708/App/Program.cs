using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;

namespace App
{
class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();
        var datePattern = config["date"];
        var timePattern = config["time"];

        var services = new ServiceCollection();
        services.AddOptions<DateTimeFormatOptions>()
            .Configure(options =>
            {
                options.DatePattern = datePattern;
                options.TimePattern = timePattern;
            })
            .Validate(options=>Validate(options.DatePattern) && Validate(options.TimePattern),"Invalid Date or Time pattern.");

        try
        {
            var options = services
                .BuildServiceProvider()
                .GetRequiredService<IOptions<DateTimeFormatOptions>>().Value;
            Console.WriteLine(options);
        }
        catch (OptionsValidationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        static bool Validate(string format)
        {
            var time = new DateTime(1981, 8, 24,2,2,2);
            var formatted = time.ToString(format);
            return DateTimeOffset.TryParseExact(formatted, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out var value) && (value.Date == time.Date || value.TimeOfDay == time.TimeOfDay);
        }
    }
}
}
