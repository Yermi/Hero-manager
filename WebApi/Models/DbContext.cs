using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        {            
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Hero> Heros { get; set; }
        public DbSet<Train> Trains { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainer>()
            .HasKey(t => t.Id);

            modelBuilder.Entity<Trainer>()
            .HasMany(t => t.Heros);
        }
    }
}