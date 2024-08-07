using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
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
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        await _checkoutService.CreateOrder(request);

        return Redirect("OrderComplete");
    }

    [Route("basket/items", Name = "modifyBasket")]
    [HttpPost]
    public async Task<IActionResult> AddItemToBasket(AddItemToBasketRequest request)
    {
        await _checkoutService.AddItemToBasket(request);

        ShopItemConstants.ItemMap.TryGetValue(request.ItemId, out var item);

        return Redirect($"/shop/category/{item?.SubCategory ?? 0}");
    }
}