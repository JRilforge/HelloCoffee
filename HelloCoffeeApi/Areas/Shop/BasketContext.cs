using HelloCoffeeApiClient.Areas.Shop.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace HelloCoffeeApi.Areas.Shop;

public class BasketContext : DbContext
{
    public BasketContext()
    {
    }

    public BasketContext(DbContextOptions<BasketContext> options)
        : base(options)
    {
    }
    
    public DbSet<CheckoutBasket> Baskets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CheckoutBasket>()
            .ToContainer("CheckoutBaskets")
            .HasPartitionKey(c => c.Id)
            .HasNoDiscriminator()
            .Property(b => b.Items)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<Guid, BasketItem>>(v));
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.ConfigureWarnings(b => b.Ignore(CosmosEventId.SyncNotSupported));
}