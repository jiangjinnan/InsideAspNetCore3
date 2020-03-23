using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace App
{
    public class CatBuilder
    {
        private readonly Cat _cat;
        public CatBuilder(Cat cat)
        {
            _cat = cat;
            _cat.Register<IServiceScopeFactory>(c => new ServiceScopeFactory(c.CreateChild()), Lifetime.Transient);
        }
        public IServiceProvider BuildServiceProvider() => _cat;
        public CatBuilder Register(Assembly assembly)
        {
            _cat.Register(assembly);
            return this;
        }

        private class ServiceScope : IServiceScope
        {
            public ServiceScope(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;
            public IServiceProvider ServiceProvider { get; }
            public void Dispose() => (ServiceProvider as IDisposable)?.Dispose();
        }

        private class ServiceScopeFactory : IServiceScopeFactory
        {
            private readonly Cat _cat;
            public ServiceScopeFactory(Cat cat) => _cat = cat;
            public IServiceScope CreateScope() => new ServiceScope(_cat);
        }
    }

}
