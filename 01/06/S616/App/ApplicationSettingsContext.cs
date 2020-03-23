using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class ApplicationSettingsContext : DbContext
    {
        public ApplicationSettingsContext(DbContextOptions options) : base(options)
        { }

        public DbSet<ApplicationSetting> Settings { get; set; }
    }
}
