using HelloCoffeeApiClient.Areas.Shop.Data.Dto;

namespace HelloCoffee.Areas.Shop;

public interface IShopService
{
    public Task<List<ShopItemDto>> GetShopItemsFor(int subCategory);
}