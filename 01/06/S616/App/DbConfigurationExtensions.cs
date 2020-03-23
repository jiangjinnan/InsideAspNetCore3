using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace App
{
    public static class DbConfigurationExtensions
    {
        public static IConfigurationBuilder AddDatabase(this IConfigurationBuilder builder, string connectionStringName,
            IDictionary<string, string> initialSettings = null)
        {
            var connectionString = builder.Build()
                .GetConnectionString(connectionStringName);
            var source = new DbConfigurationSource(optionsBuilder => optionsBuilder.UseSqlServer(connectionString),
                initialSettings);
            builder.Add(source);
            return builder;
        }
    }

}
