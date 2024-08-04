using HelloCoffee.Areas.Identity.Data;
using HelloCoffee.Areas.Shop;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HelloCoffee.Data;

public class HelloCoffeeContext : IdentityDbContext<HelloCoffeeUser>
{
    public HelloCoffeeContext(DbContextOptions<HelloCoffeeContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //builder.HasDefaultContainer("HelloCoffeeContainer");

        builder.Entity<HelloCoffeeUser>()
            .ToContainer("HelloCoffeeContainer")
            .HasPartitionKey(c => c.UserId)
            .HasDiscriminator<int>("Type")
            .HasValue<HelloCoffeeUser>(0);
    
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<IdentityRole>()
            .Property(b => b.ConcurrencyStamp)
            .IsETagConcurrency();
        builder.Entity<HelloCoffeeUser>() // ApplicationUser mean the Identity user 'ApplicationUser : IdentityUser'
            .Property(b => b.ConcurrencyStamp)
            .IsETagConcurrency();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.ConfigureWarnings(b => b.Ignore(CosmosEventId.SyncNotSupported));
}
