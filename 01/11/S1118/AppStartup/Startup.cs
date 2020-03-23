using Microsoft.AspNetCore.Builder;
using System;

namespace AppStartup
{
    public abstract class StartupBase
    {
        public StartupBase() => Console.WriteLine(this.GetType().FullName);
        public void Configure(IApplicationBuilder app) { }
    }

    public class StartupDevelopment : StartupBase { }
    public class StartupStaging : StartupBase { }
    public class Startup : StartupBase { }

}
