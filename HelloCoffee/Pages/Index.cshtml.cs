using HelloCoffee.Areas.Identity.Data;
using HelloCoffee.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos.Linq;

namespace HelloCoffee.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly IShopService _shopService;

    private readonly ICheckoutService _checkoutService;

    private readonly UserManager<HelloCoffeeUser> _userManager;

    private readonly SignInManager<HelloCoffeeUser> _signInManager;

    private Dictionary<Guid, BasketItem> _basketItems { get; set; } = new();
    
    [BindProperty]
    public List<ShopItemDto> Items { get; set; }

    [BindProperty]
    public int SelectedSubCategory { get; set; }

    [BindProperty]
    public int BasketItemCount { get; set; }

    public IndexModel(ILogger<IndexModel> logger, 
        IShopService shopService, 
        ICheckoutService checkoutService,
        UserManager<HelloCoffeeUser> userManager,
        SignInManager<HelloCoffeeUser> signInManager)
    {
        _logger = logger;
        Items = new();
        _shopService = shopService;
        _checkoutService = checkoutService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task OnGet()
    {
        string[] pathSegments = Request.Path.Value?.Split("/") ?? [];

        string? subCategoryString = pathSegments.Length > 0 ? pathSegments[^1] : "0";

        int.TryParse(subCategoryString, out var subCategory);

        SelectedSubCategory = subCategory;
        
        Items = await _shopService.GetShopItemsFor(subCategory);

        if (_signInManager.IsSignedIn(User))
        {
            var userId = _userManager.GetUserId(User);
                
            var basket = await _checkoutService.GetBasket(Guid.Parse(userId ?? ""));

            _basketItems = basket.Items;

            foreach (var item in _basketItems)
            {
                BasketItemCount += item.Value.UnitCount;
            }
        }
    }

    public int GetBasketItemUnitCount(Guid id)
    {
        _basketItems.TryGetValue(id, out var item);

        return item?.UnitCount ?? 0;
    }
}