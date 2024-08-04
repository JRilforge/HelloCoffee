using Microsoft.EntityFrameworkCore;

namespace HelloCoffee.Areas.Shop;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
    {
    }
    
    public DbSet<ShopItem> Items { get; set; }
}