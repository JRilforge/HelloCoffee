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
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.ConfigureWarnings(b => b.Ignore(CosmosEventId.SyncNotSupported));
}