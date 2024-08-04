namespace HelloCoffee.Areas.Shop;

public class AddItemToBasketRequest
{
    public Guid UserId { get; set; }
    public Guid ItemId { get; set; }
    public uint UnitCountModification { get; set; }
}