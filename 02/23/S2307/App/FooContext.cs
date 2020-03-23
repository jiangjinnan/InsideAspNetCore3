using Microsoft.EntityFrameworkCore;

namespace App
{
    public class FooContext : DbContext
    {
        public FooContext(DbContextOptions<FooContext> options) : base(options)
        { }
    }
}