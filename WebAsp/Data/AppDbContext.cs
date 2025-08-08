using Microsoft.EntityFrameworkCore;
using WebAsp.Models;

namespace WebAsp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Set a longer command timeout (e.g., 120 seconds) to avoid timeouts during initialization
            Database.SetCommandTimeout(TimeSpan.FromSeconds(120));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for Actor_Movie
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            // Configure relationships
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie)
                .WithMany(am => am.Actor_Movies)
                .HasForeignKey(m => m.MovieId);

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor)
                .WithMany(am => am.Actor_Movies)
                .HasForeignKey(m => m.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        // DbSet properties
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actor_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
    }
}