using Microsoft.EntityFrameworkCore;

namespace App
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(user => user.NormalizedUserName);
                builder.HasMany(user => user.Roles).WithOne().HasForeignKey(
                    userRole => userRole.NormalizedUserName);
            });
            modelBuilder.Entity<Role>(builder =>
            {
                builder.HasKey(role => role.NormalizedRoleName);
                builder.HasMany(role => role.Users).WithOne().HasForeignKey(
                    userRole => userRole.NormalizedRoleName);
            });
            modelBuilder.Entity<UserRole>(builder => builder.HasKey(
                userRole => new { userRole.NormalizedUserName, userRole.NormalizedRoleName }));
        }
        public UserDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            if (Users.Find("FOO") == null)
            {
                Users.Add(new User("Foo", "password"));
                Users.Add(new User("Bar", "password"));
                Roles.Add(new Role("Admin"));
                UserRoles.Add(new UserRole
                {
                    NormalizedUserName = "BAR",
                    NormalizedRoleName = "ADMIN"
                });
                SaveChanges();
            }
        }
    }
}
