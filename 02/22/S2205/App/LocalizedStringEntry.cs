using System.Collections.Generic;
using System.Globalization;

namespace App
{
    public class LocalizedStringEntry
    {
        public string Value { get; set; }
        public IDictionary<CultureInfo, string> Translations { get; set; }
    }
}