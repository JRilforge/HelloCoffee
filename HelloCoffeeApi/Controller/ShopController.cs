using HelloCoffee.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelloCoffeeApi.Controller;

public class ShopController : ControllerBase
{
    // Get Items - filtered
    [HttpGet("shop/{category}/{subCategory}")]
    public List<ShopItemDto> GetShopItemsFor(int category, int subCategory)
    {
        return null;
    }
}