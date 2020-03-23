using System;
using System.Collections.Generic;

namespace App
{
    public class WeatherReport
    {
        private static string[] _conditions = new string[] { "晴", "多云", "小雨" };
        private static Random _random = new Random();

        public string City { get; }
        public IDictionary<DateTime, WeatherInfo> WeatherInfos { get; }

        public WeatherReport(string city, int days)
        {
            City = city;
            WeatherInfos = new Dictionary<DateTime, WeatherInfo>();
            for (int i = 0; i < days; i++)
            {
                this.WeatherInfos[DateTime.Today.AddDays(i + 1)] = new WeatherInfo
                {
                    Condition = _conditions[_random.Next(0, 2)],
                    HighTemperature = _random.Next(20, 30),
                    LowTemperature = _random.Next(10, 20)
                };
            }
        }

        public WeatherReport(string city, DateTime date)
        {
            City = city;
            WeatherInfos = new Dictionary<DateTime, WeatherInfo>
            {
                [date] = new WeatherInfo
                {
                    Condition = _conditions[_random.Next(0, 2)],
                    HighTemperature = _random.Next(20, 30),
                    LowTemperature = _random.Next(10, 20)
                }
            };
        }

        public class WeatherInfo
        {
            public string Condition { get; set; }
            public double HighTemperature { get; set; }
            public double LowTemperature { get; set; }
        }
    }

}