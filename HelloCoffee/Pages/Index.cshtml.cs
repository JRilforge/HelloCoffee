using HelloCoffee.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos.Linq;

namespace HelloCoffee.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly IShopService _shopService;
    
    [BindProperty]
    public List<ShopItemDto> Items { get; set; }

    [BindProperty]
    public int SelectedSubCategory { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IShopService shopService)
    {
        _logger = logger;
        Items = new();
        _shopService = shopService;
    }

    public async Task OnGet()
    {
        string[] pathSegments = Request.Path.Value?.Split("/") ?? [];

        string? subCategoryString = pathSegments.Length > 0 ? pathSegments[^1] : "0";

        int.TryParse(subCategoryString, out var subCategory);

        SelectedSubCategory = subCategory;
        
        Items = await _shopService.GetShopItemsFor(subCategory);
    }
}