using System.Net;
using HelloCoffee.Areas.Shop;
using HelloCoffeeApi.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HelloCoffeeApi.Controller;

public class CheckoutController : ControllerBase
{
    private readonly BasketContext _basketContext;
    private readonly OrderContext _orderContext;
    private readonly ShopContext _shopContext;
    
    public CheckoutController(BasketContext basketContext, OrderContext orderContext, ShopContext shopContext)
    {
        _basketContext = basketContext;
        _orderContext = orderContext;
        _shopContext = shopContext;
    }
    
    // Add Item to Basket
    [HttpPost("basket/items")]
    public async Task<IActionResult> AddItemToBasket([FromBody] AddItemToBasketRequest request)
    {
        await using var context = _basketContext;
        
        var basket = await context.Baskets.Where(nextBasket =>
            nextBasket.UserId == request.UserId).FirstOrDefaultAsync() ?? new ()
        {
            UserId = request.UserId
        };

        var item = await _shopContext.Items.Where(item => item.Id == request.ItemId).FirstOrDefaultAsync();

        if (item == null)
        {
            return BadRequest("Item doesn't exist"); 
        }
            
        if (!basket.Items.ContainsKey(request.ItemId))
        {
            basket.Items[request.ItemId] = new()
            {
                ItemId = item.Id,
                UnitCost = item.Price
            };
        }
                
        basket.Items[request.ItemId].UnitCount += request.UnitCountModification;

        if (basket.Id == Guid.Empty)
        {
            await context.AddAsync(basket);
        }
        else
        {
            context.Update(basket);
        }
            
        var updateCount = await context.SaveChangesAsync(true);

        return Ok(updateCount > 0);
    }
    
    // Get Basket
    [HttpGet("basket")]
    public async Task<CheckoutBasketDto> GetBasket(Guid userId)
    {
        await using var context = _basketContext;
        
        var basket = await context.Baskets.Where(basket => basket.UserId == userId).FirstOrDefaultAsync() ?? new ();

        return new CheckoutBasketDto()
        {
            Id = basket.Id,
            UserId = basket.UserId,
            Items = basket.Items
        };
    }
    
    // Create Order
    [HttpPost("orders")]
    public async Task<bool> CreateOrder([FromBody] CreateOrderRequest orderRequest)
    {
        await using var context = _orderContext;
        await using var basketContext = _basketContext;
        
        CheckoutBasket? basket = await basketContext.Baskets.Where(retrievedBasket =>
            retrievedBasket.UserId == orderRequest.UserId).FirstOrDefaultAsync();

        if (basket == null || basket.Items.Count == 0)
        {
            HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                
            return false;
        }

        var totalPrice = 0.0;

        foreach (var basketItem in basket.Items.Values)
        {
            totalPrice += basketItem.UnitCost * basketItem.UnitCount;
        }

        var addResult = await context.Orders.AddAsync(new ()
        {
            Id = Guid.NewGuid(),
            UserId = orderRequest.UserId,
            FirstName = orderRequest.FirstName,
            LastName = orderRequest.LastName,
            DeliveryAddress = orderRequest.Address,
            BasketItems = basket.Items,
            Paid = true,
            TotalPrice = totalPrice
        });
        await context.SaveChangesAsync();
        
        basket.Items.Clear();
        basketContext.Update(basket);
        await basketContext.SaveChangesAsync();
                
        return addResult?.Entity != null;;
    }
    
    // Get Orders
    [HttpGet("orders")]
    public async Task<List<OrderDto>> GetOrders(Guid userId)
    {
        await using var context = _orderContext;
        
        var orders = await context.Orders.Where(order => order.UserId == userId).ToListAsync();

        return orders.ConvertAll(order => new OrderDto()
        {
            Id = order.Id,
            BasketItems = order.BasketItems,
            Paid = order.Paid
        });
    }
}