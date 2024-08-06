namespace HelloCoffeeApiClient.Areas.Shop.Data.Dto;

public class OrderDto
{
    public Guid Id { get; set; }

    public Dictionary<Guid, BasketItem> BasketItems { get; set; } = new();
    public bool Paid { get; set; } = false;
}