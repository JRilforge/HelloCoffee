using HelloCoffeeApiClient.Areas.Shop.Data.Dto;

namespace HelloCoffee.Areas.Shop;

public class ClientShopService(HttpClient client) : IShopService
{
    public async Task<List<ShopItemDto>> GetShopItemsFor(int category, int subCategory)
    {
        return await client.GetFromJsonAsync<List<ShopItemDto>>($"shop/{category}/{subCategory}") ?? [];
    }
}