using HelloCoffee.Areas.Shop;
using HelloCoffeeApi.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloCoffeeApi.Controller;

public class ShopController : ControllerBase
{
    // Get Items - filtered
    [HttpGet("shop/{category}/{subCategory}")]
    public async Task<List<ShopItemDto>> GetShopItemsFor(uint category, uint subCategory)
    {
        List<ShopItem> shopItems;
        
        using (var context = new ShopContext())
        {
            shopItems = await context.Items.Where(
                    e => ((int) e.Category) == category
                         && ((int) e.SubCategory) == subCategory)
                .ToListAsync();
            Console.WriteLine($"{shopItems.Count} shop items found for " +
                              $"category {((ItemCategory)category).ToString()} and " +
                              $"sub category {((ItemSubCategory)subCategory).ToString()}");
            Console.WriteLine();
        }

        return shopItems.ConvertAll(item => new ShopItemDto()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price
        });
    }
}