using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloCoffee.Pages;

public class OrderCompleteModel : PageModel
{
    private readonly ILogger<OrderCompleteModel> _logger;

    [BindProperty]
    public int BasketItemCount { get; set; }

    public OrderCompleteModel(ILogger<OrderCompleteModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}