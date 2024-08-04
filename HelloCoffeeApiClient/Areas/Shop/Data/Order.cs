namespace HelloCoffeeApiClient.Areas.Shop.Data;

public class Order
{
    public Guid Id { get; set; }

    public Dictionary<Guid, int> ItemsAndQuantities { get; set; } = new();
    public bool Paid { get; set; } = false;
}