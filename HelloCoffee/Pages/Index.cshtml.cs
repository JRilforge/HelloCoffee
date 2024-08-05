using HelloCoffee.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos.Linq;

namespace HelloCoffee.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly IShopService _shopService;
    
    [BindProperty]
    public LinkedList<ShopItemDto> Items { get; set; }

    [BindProperty]
    public string[] ItemNames { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IShopService shopService)
    {
        _logger = logger;
        Items = new();
        _shopService = shopService;
    }

    public void OnGet()
    {
        ItemNames = new[]
        {
            "Drink1", "Drink2"
        };
        
        string[] pathSegments = Request.Path.Value?.Split("/") ?? [];

        string? subCategoryString = pathSegments.Length > 0 ? pathSegments[^1] : null;
        string? categoryString = pathSegments.Length > 1 ? pathSegments[^2] : null;

        int subCategory = 0, category = 0;

        int.TryParse(subCategoryString, out subCategory);
        int.TryParse(categoryString, out category);
        
        //TODO ItemNames = _shopService.GetShopItemsFor(category, subCategory);
        
        foreach (var name in ItemNames) {
            Items.AddLast(new ShopItemDto
            {
                Name = name
            });
        }
    }
}