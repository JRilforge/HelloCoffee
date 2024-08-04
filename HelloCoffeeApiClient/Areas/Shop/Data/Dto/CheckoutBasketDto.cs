using HelloCoffeeApiClient.Areas.Shop.Data;

namespace HelloCoffee.Areas.Shop;

public class CheckoutBasketDto
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public Dictionary<Guid, BasketItem> Items { get; set; } = new();
}