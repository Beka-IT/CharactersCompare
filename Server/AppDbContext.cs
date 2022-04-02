
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users {get;set;}
        public DbSet<Character> Characters {get;set;}
        public DbSet<LoginModel> LoginModels { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasOne(a => a.LoginModel).WithOne(b => b.User)
                    .HasForeignKey<LoginModel>(e => e.Id);
                modelBuilder.Entity<User>().ToTable("User");
                modelBuilder.Entity<LoginModel>().ToTable("User");
        }
    }
}