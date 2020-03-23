using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace App
{
    public class DictionaryStringLocalizer : IStringLocalizer
    {
        private readonly Dictionary<string, LocalizedStringEntry> _entries;

        public DictionaryStringLocalizer(Dictionary<string, LocalizedStringEntry> entries)
        {
            _entries = new Dictionary<string, LocalizedStringEntry>(entries);
        }

        public LocalizedString this[string name] => GetString(name,  CultureInfo.CurrentUICulture);

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var raw = this[name];
                return raw.ResourceNotFound
                    ? raw
                    : new LocalizedString(name, string.Format(raw.Value, arguments));
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var culture = CultureInfo.CurrentUICulture;
            foreach (var item in _entries)
            {
                if (includeParentCultures)
                {
                    yield return GetString(item.Key, culture);
                }
                else
                {
                    yield return item.Value.Translations.TryGetValue(culture, out var text)
                        ? new LocalizedString(item.Key, text)
                        : new LocalizedString(item.Key, item.Key, true);
                }
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture) => throw new NotImplementedException();

        private LocalizedString GetString(string name, CultureInfo culture)
        {
            if (!_entries.TryGetValue(name, out var entry))
            {
                return new LocalizedString(name, name, true);
            }

            if (entry.Translations.TryGetValue(culture, out var message))
            {
                return new LocalizedString(name, message);
            }

            if (culture == CultureInfo.InvariantCulture)
            {
                return new LocalizedString(name, entry.Value, true);
            }

            return GetString(name, culture.Parent);
        }
    }

}