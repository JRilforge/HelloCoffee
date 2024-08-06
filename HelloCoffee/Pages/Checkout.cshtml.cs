using HelloCoffee.Areas.Identity.Data;
using HelloCoffee.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloCoffee.Pages;

public class CheckoutModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly IShopService _shopService;

    private readonly ICheckoutService _checkoutService;

    private readonly UserManager<HelloCoffeeUser> _userManager;

    private readonly SignInManager<HelloCoffeeUser> _signInManager;
    
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = "John";
    public string LastName { get; set; } = "Doe";

    public Address Address { get; set; } = new()
    {
        FirstLine = "1 Imaginary Street",
        SecondLine = "Fake Avenue",
        City = "London",
        County = "Greater London",
        Country = "United Kingdom",
        PostCode = "ST6 2AH"
    };

    public string CardNumber { get; set; } = "7894-3244-3242-5489";
    public string NameOnCard { get; set; } = "Mr John Doe";
    public string Expiration { get; set; } = "12/2040";
    public string Cvv { get; set; } = "368";

    [BindProperty]
    public int BasketItemCount { get; set; }

    [BindProperty]
    public double TotalCost { get; set; }
    
    [BindProperty]
    public Dictionary<Guid, BasketItem> BasketItems { get; set; } = new();
    
    public CheckoutModel(ILogger<IndexModel> logger, 
        IShopService shopService, 
        ICheckoutService checkoutService,
        UserManager<HelloCoffeeUser> userManager,
        SignInManager<HelloCoffeeUser> signInManager)
    {
        _logger = logger;
        _shopService = shopService;
        _checkoutService = checkoutService;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task OnGet()
    {
        if (_signInManager.IsSignedIn(User))
        {
            var userId = _userManager.GetUserId(User);
                
            var basket = await _checkoutService.GetBasket(Guid.Parse(userId ?? ""));

            BasketItems = basket.Items;

            foreach (var item in BasketItems)
            {
                BasketItemCount += item.Value.UnitCount;

                TotalCost += item.Value.UnitCount * item.Value.UnitCost;
            }
        }
    }
}