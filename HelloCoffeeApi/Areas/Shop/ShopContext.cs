using HelloCoffeeApiClient.Areas.Shop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HelloCoffeeApi.Areas.Shop;

public class ShopContext : DbContext
{
    public ShopContext()
    {
    }

    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }
    
    public DbSet<ShopItem> Items { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ShopItem>()
            .ToContainer("ShopItems")
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();
        
        builder.Entity<ShopItem>()
            .Property(o => o.Id)
            .ToJsonProperty("id");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.ConfigureWarnings(b => b.Ignore(CosmosEventId.SyncNotSupported));
}