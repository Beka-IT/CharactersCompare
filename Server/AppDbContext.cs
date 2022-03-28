
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users {get;set;}
        public DbSet<Character> Characters {get;set;}
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    }
}