using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Models;
using Domain.Models.Animal;

namespace Domain.Data
{
    public class AnimalDbContext : DbContext
    {
        public DbSet<AnimalModel> AnimalModels { get; set; }
        public DbSet<Bird> Birds { get; set; }
        public virtual DbSet<Dog> Dogs { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public DbSet<LoginRequests> LoginRequests { get; set; }
        public DbSet<UserAnimal> UserAnimals { get; set; }

        private readonly IConfiguration _configuration;

        public AnimalDbContext() // Parameterlös konstruktor
        {

        }
        public AnimalDbContext(DbContextOptions<AnimalDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        // Konfigurera relationer i OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfigurera många-till-många-relationen mellan User och AnimalModel via UserAnimal
            modelBuilder.Entity<UserAnimal>()
                .HasKey(ua => new { ua.UserId, ua.AnimalId });

            modelBuilder.Entity<UserAnimal>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAnimals)
                .HasForeignKey(ua => ua.UserId);

            modelBuilder.Entity<UserAnimal>()
                .HasOne(ua => ua.Animal)
                .WithMany(a => a.UserAnimals)
                .HasForeignKey(ua => ua.AnimalId);

            modelBuilder.Entity<UserAnimal>()
                .Property(ua => ua.UserName)
                .IsRequired();

            modelBuilder.Entity<UserAnimal>()
                .Property(ua => ua.Name)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserAnimals)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId);
        }
    }
}