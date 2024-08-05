using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloCoffeeApiClient.Areas.Shop.Data;

public class CheckoutBasket
{
    [Key] 
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public Dictionary<Guid, BasketItem> Items { get; set; } = new();
}

public class BasketItem
{
    [Key]
    public Guid ItemId { get; set; }
    public uint UnitCount { get; set; }
    public double UnitCost { get; set; }
}