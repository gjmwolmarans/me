using Microsoft.EntityFrameworkCore;

namespace me;

public class MeDbContext : DbContext
{
    public MeDbContext(DbContextOptions<MeDbContext> options) : base(options)
    {
        
    }

    public DbSet<Provider> Providers { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ResourceTag> ResourceTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Provider>()
            .HasMany(p => p.Resources)
            .WithOne(r => r.Provider);
        
        modelBuilder.Entity<Resource>();

        modelBuilder.Entity<Tag>();

        modelBuilder.Entity<ResourceTag>()
            .HasOne(rt => rt.Resource)
            .WithMany(r => r.ResourceTags);
        modelBuilder.Entity<ResourceTag>()
            .HasOne(rt => rt.Tag)
            .WithMany(t => t.ResourceTags);
    }
}
