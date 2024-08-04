namespace HelloCoffeeApiClient.Areas.Shop.Data;

public class ShopItem
{
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    
    public string Name { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; } = 0.0;

    public ItemCategory Category { get; set; }
    public ItemSubCategory SubCategory { get; set; }
}