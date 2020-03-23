using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class DateTimeFormatOptions
    {
        public string DatePattern { get; set; }
        public string TimePattern { get; set; }
        public override string ToString() => $"Date: {DatePattern}; Time: {TimePattern}";
    }

}
