using AuthAPI.Services.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services.DB
{
    public class Context : DbContext
    {
        public DbSet<User> Users {get; set;} 
    
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }
    }
}
