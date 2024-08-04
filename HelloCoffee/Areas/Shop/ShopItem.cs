namespace HelloCoffee.Areas.Shop;

public class ShopItem
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    
    public string Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; } = 0.0;
    public string PriceAsCurrency { get => string.Format("{0:c}", Price); }
}