namespace HelloCoffee.Areas.Shop;

public class OrderDto
{
    public Guid Id { get; set; }

    public Dictionary<Guid, int> ItemsAndQuantities { get; set; } = new();
    public bool Paid { get; set; } = false;
}