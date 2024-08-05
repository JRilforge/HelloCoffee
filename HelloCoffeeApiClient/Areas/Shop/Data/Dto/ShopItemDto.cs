namespace HelloCoffeeApiClient.Areas.Shop.Data.Dto;

public class ShopItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double? Price { get; set; } = 0.0;
    public string PriceAsCurrency { get => string.Format("{0:c}", Price); }
}