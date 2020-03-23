using Microsoft.EntityFrameworkCore;

namespace App
{
    public class BarContext : DbContext
    {
        public BarContext(DbContextOptions<BarContext> options) : base(options)
        { }
    }
}