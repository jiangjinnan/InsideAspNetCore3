using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App
{
    public class DbConfigurationProvider : ConfigurationProvider
    {
        private readonly IDictionary<string, string> _initialSettings;
        private readonly Action<DbContextOptionsBuilder> _setup;

        public DbConfigurationProvider(Action<DbContextOptionsBuilder> setup,
            IDictionary<string, string> initialSettings)
        {
            _setup = setup;
            _initialSettings = initialSettings ?? new Dictionary<string, string>();
        }

        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<ApplicationSettingsContext>();
            _setup(builder);
            using (ApplicationSettingsContext dbContext = new ApplicationSettingsContext(builder.Options))
            {
                dbContext.Database.EnsureCreated();
                Data = dbContext.Settings.Any()
                    ? dbContext.Settings.ToDictionary(it => it.Key, it => it.Value, StringComparer.OrdinalIgnoreCase)
                    : Initialize(dbContext);
            }
        }

        private IDictionary<string, string> Initialize(ApplicationSettingsContext dbContext)
        {
            foreach (var item in _initialSettings)
            {
                dbContext.Settings.Add(new ApplicationSetting(item.Key, item.Value));
            }
            return _initialSettings.ToDictionary(it => it.Key, it => it.Value, StringComparer.OrdinalIgnoreCase);
        }
    }

}