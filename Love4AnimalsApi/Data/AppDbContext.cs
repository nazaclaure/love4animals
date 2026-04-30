using Love4AnimalsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Love4AnimalsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Campaign> Campaigns => Set<Campaign>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);
                e.Property(u => u.Name).IsRequired().HasMaxLength(100);
                e.Property(u => u.Email).IsRequired().HasMaxLength(200);
                e.Property(u => u.Password).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Campaign>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
                e.Property(c => c.Description).IsRequired().HasMaxLength(500);
                e.Property(c => c.FundraisingGoal).HasPrecision(18, 2);
                e.Property(c => c.TotalRaised).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Post>(e =>
            {
                e.HasKey(p => p.Id);
                e.Property(p => p.Description).IsRequired();
                e.Property(p => p.ImageURL).IsRequired();
                e.HasOne<User>().WithMany().HasForeignKey(p => p.UserId);
                e.HasOne<Campaign>().WithMany().HasForeignKey(p => p.CampaignId);
            });

            modelBuilder.Entity<Comment>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Content).IsRequired();
                e.HasOne<Post>().WithMany().HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne<User>().WithMany().HasForeignKey(c => c.UserId);
            });
        }
    }
}
