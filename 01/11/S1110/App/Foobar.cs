using System;

namespace App
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public class Base : IDisposable
    {
        public Base() => Console.WriteLine($"{this.GetType().Name} is created.");
        public void Dispose() => Console.WriteLine($"{this.GetType().Name} is disposed.");
    }
    public class Foo : Base, IFoo { }
    public class Bar : Base, IBar { }
    public class Baz : Base, IBaz { }

}
