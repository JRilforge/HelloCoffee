using HelloCoffeeApiClient.Areas.Shop.Data;

namespace HelloCoffee.Areas.Shop;

public class OrderDto
{
    public Guid Id { get; set; }

    public Dictionary<Guid, BasketItem> BasketItems { get; set; } = new();
    public bool Paid { get; set; } = false;
}