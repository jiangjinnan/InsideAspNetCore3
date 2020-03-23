using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class SingletonService
    {
        public IServiceProvider ApplicationServices { get; }
        public SingletonService(IServiceProvider applicationServices)=> ApplicationServices = applicationServices;
    }

    public class ScopedService
    {
        public IServiceProvider RequestServices { get; }
        public ScopedService(IServiceProvider requestServices) => RequestServices = requestServices;
    }

}
