using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace App
{
    [TypeConverter(typeof(PointTypeConverter))]
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

}
