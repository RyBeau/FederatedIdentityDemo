using AuthAPI.Services.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services.DB
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Role>().ToTable("roles");

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);

                user.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                user.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(role =>
            {
                role.HasKey(r => r.Id);
                role.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
