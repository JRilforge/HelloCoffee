using HelloCoffeeApiClient.Areas.Shop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HelloCoffeeApi.Areas.Shop;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }
    
    public DbSet<ShopItem> Items { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //builder.HasDefaultContainer("HelloCoffeeContainer");

        builder.Entity<ShopItem>()
            .ToContainer("HelloCoffeeContainer")
            .HasPartitionKey(c => c.Id)
            .HasDiscriminator<int>("Type")
            .HasValue<ShopItem>(0);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.ConfigureWarnings(b => b.Ignore(CosmosEventId.SyncNotSupported));
}