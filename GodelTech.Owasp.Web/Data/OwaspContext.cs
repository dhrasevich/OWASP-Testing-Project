using GodelTech.Owasp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace GodelTech.Owasp.Web.Data
{
    public class OwaspContext : DbContext
    {
        public OwaspContext()
        {
        }

        public OwaspContext(DbContextOptions<OwaspContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<User> Users { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Album>().ToTable("Album");
        //     modelBuilder.Entity<Artist>().ToTable("Artist");
        //     modelBuilder.Entity<Genre>().ToTable("Genre");
        //     modelBuilder.Entity<User>().ToTable("User");
        // }
    }
}