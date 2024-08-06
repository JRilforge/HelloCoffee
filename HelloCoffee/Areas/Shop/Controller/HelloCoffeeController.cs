using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HelloCoffee.Areas.Shop.Controller;

public class HelloCoffeeController : ControllerBase
{
    private readonly ICheckoutService _checkoutService;
    
    public HelloCoffeeController(ICheckoutService checkoutService)
    {
        _checkoutService = checkoutService;
    }

    [Route("orders", Name = "createOrder")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        await _checkoutService.CreateOrder(request);

        return Redirect("OrderComplete");
    }
}