using HelloCoffee.Areas.Shop;
using HelloCoffeeApi.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using Microsoft.AspNetCore.Mvc;

namespace HelloCoffeeApi.Controller;

public class CheckoutController : ControllerBase
{
    // Add Item to Basket
    [HttpPost("basket/items")]
    public bool AddItemToBasket(AddItemToBasketRequest addItemToBasketRequest)
    {
        // TODO Implement
        return true;
    }
    
    // Get Basket
    [HttpGet("basket")]
    public CheckoutBasket GetBasket()
    {
        // TODO Implement
        return null;
    }
    
    // Create Order
    [HttpPost("orders")]
    public OrderDto CreateOrder(CreateOrderRequest orderRequest)
    {
        // TODO Implement
        return null;
    }
    
    // Get Orders
    [HttpGet("orders")]
    public List<OrderDto> GetOrders()
    {
        // TODO Implement
        return null;
    }
}