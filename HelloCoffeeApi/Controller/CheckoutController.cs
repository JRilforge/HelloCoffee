using HelloCoffee.Areas.Shop;
using HelloCoffeeApi.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HelloCoffeeApi.Controller;

public class CheckoutController : ControllerBase
{
    private BasketContext BasketContext;
    private OrderContext OrderContext;
    
    public CheckoutController(BasketContext basketContext, OrderContext orderContext)
    {
        BasketContext = basketContext;
        OrderContext = orderContext;
    }
    
    // Add Item to Basket
    [HttpPost("basket/items")]
    public async Task<bool> AddItemToBasket(AddItemToBasketRequest request)
    {
        using (var context = BasketContext)
        {
            var basket = await context.Baskets.Where(basket => basket.UserId == request.UserId).FirstOrDefaultAsync();

            if (basket != null)
            {
                if (!basket.Items.ContainsKey(request.ItemId))
                {
                    basket.Items[request.ItemId] = new();
                }
                
                basket.Items[request.ItemId].UnitCount += request.UnitCountModification;
                
                context.Update(basket.Items[request.ItemId]);
                await context.SaveChangesAsync(true);
                
                Console.WriteLine();

                return false;
            }
        }
        return true;
    }
    
    // Get Basket
    [HttpGet("basket")]
    public async Task<CheckoutBasketDto> GetBasket(Guid userId)
    {
        using (var context = BasketContext)
        {
            var basket = await context.Baskets.Where(basket => basket.UserId == userId).FirstOrDefaultAsync() ?? new CheckoutBasket();

            return new CheckoutBasketDto()
            {
                Id = basket.Id,
                UserId = basket.UserId,
                Items = basket.Items
            };
        }
    }
    
    // Create Order
    [HttpPost("orders")]
    public async Task<bool> CreateOrder(CreateOrderRequest orderRequest)
    {
        using (var context = OrderContext)
        {
            CheckoutBasket basket;
            
            using (var basketContext = BasketContext)
            {
                basket = await basketContext.Baskets.Where(retrievedBasket => 
                    retrievedBasket.UserId == orderRequest.UserId).FirstOrDefaultAsync() ?? new CheckoutBasket();
            }

            var addResult = await context.Orders.AddAsync(new ()
            {
                Id = orderRequest.Id,
                UserId = orderRequest.UserId,
                BasketItems = basket.Items,
                Paid = true
            });
                
            return addResult?.Entity != null;;
        }

        return false;
    }
    
    // Get Orders
    [HttpGet("orders")]
    public async Task<List<OrderDto>> GetOrders(Guid userId)
    {
        using (var context = OrderContext)
        {
            var orders = await context.Orders.Where(order => order.UserId == userId).ToListAsync();

            return orders.ConvertAll(order => new OrderDto()
            {
                Id = order.Id,
                BasketItems = order.BasketItems,
                Paid = order.Paid
            });
        }
    }
}