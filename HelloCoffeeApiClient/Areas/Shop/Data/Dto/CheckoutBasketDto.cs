namespace HelloCoffeeApiClient.Areas.Shop.Data.Dto;

public class CheckoutBasketDto
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public Dictionary<Guid, BasketItem> Items { get; set; } = new();
}