using Microsoft.EntityFrameworkCore;
using TravelAdvisor.Backend.Models;

namespace TravelAdvisor.Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Place> Places { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserPlaceOpinion> UserPlaceOpinions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Place>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Tag>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<OpeningHours>()
                .HasKey(oh => oh.Id);

            modelBuilder.Entity<Image>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserPlaceOpinion>()
                .HasKey(uo => uo.Id);

            modelBuilder.Entity<Place>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Places);

            modelBuilder.Entity<Place>()
                .HasMany(p => p.OpeningHours)
                .WithOne(oh => oh.Place)
                .HasForeignKey(oh => oh.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Place>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Place)
                .HasForeignKey(i => i.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Place>()
                .HasMany(p => p.UserOpinions)
                .WithOne(uo => uo.Place)
                .HasForeignKey(uo => uo.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Opinions)
                .WithOne(uo => uo.User)
                .HasForeignKey(uo => uo.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 