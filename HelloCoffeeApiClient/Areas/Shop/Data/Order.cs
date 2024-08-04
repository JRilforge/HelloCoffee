namespace HelloCoffeeApiClient.Areas.Shop.Data;

public class Order
{
    public Guid Id { get; set; }

    public Dictionary<Guid, BasketItem> BasketItems { get; set; } = new();
    public bool Paid { get; set; } = false;
    public Guid UserId { get; set; }
}