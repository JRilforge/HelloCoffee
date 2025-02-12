namespace HelloCoffeeApiClient.Areas.Shop.Data;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";

    public Dictionary<Guid, BasketItem> BasketItems { get; set; } = new();
    public bool Paid { get; set; }
    
    public double TotalPrice { get; set; }

    public Address DeliveryAddress { get; set; } = new ();
}