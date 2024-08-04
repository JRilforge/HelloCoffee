using HelloCoffee.Areas.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloCoffee.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    [BindProperty]
    public LinkedList<ShopItem> Items { get; set; }

    [BindProperty]
    public string[] ItemNames { get; set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        Items = new();
    }

    public void OnGet()
    {
        ItemNames = new[]
        {
            "Drink1", "Drink2"
        };
        
        foreach (var name in ItemNames) {
            Items.AddLast(new ShopItem
            {
                Name = name
            });
        }
    }
}