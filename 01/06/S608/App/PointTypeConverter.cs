using System;
using System.ComponentModel;
using System.Globalization;

namespace App
{
    public class PointTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        => sourceType == typeof(string);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string[] split = value.ToString().Split(',');
            double x = double.Parse(split[0].Trim().TrimStart('('));
            double y = double.Parse(split[1].Trim().TrimEnd(')'));
            return new Point { X = x, Y = y };
        }
    }

}