using HelloCoffeeApiClient.Areas.Shop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace HelloCoffeeApi.Areas.Shop;

public class OrderContext : DbContext
{
    public OrderContext()
    {
    }

    public OrderContext(DbContextOptions<OrderContext> options)
        : base(options)
    {
    }
    
    public DbSet<Order> Orders { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Order>()
            .ToContainer("Orders")
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator()
            .Property(b => b.BasketItems)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<Guid, BasketItem>>(v));
        
        builder.Entity<BasketItem>()
            .ToContainer(nameof(BasketItem))
            .HasPartitionKey(c => c.ItemId)
            .HasNoDiscriminator();
        
        builder.Entity<Address>()
            .ToContainer(nameof(Address))
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator();
        
        builder.Entity<Order>()
            .Property(o => o.Id)
            .ToJsonProperty("id");
        
        builder.Entity<BasketItem>()
            .Property(o => o.ItemId)
            .ToJsonProperty("id");
        
        builder.Entity<Address>()
            .Property(o => o.Id)
            .ToJsonProperty("id");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.ConfigureWarnings(b => b.Ignore(CosmosEventId.SyncNotSupported));
}