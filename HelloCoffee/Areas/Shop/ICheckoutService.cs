using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HelloCoffee.Areas.Shop;

public interface ICheckoutService
{
    public Task<bool> AddItemToBasket(AddItemToBasketRequest request);
    
    public Task<CheckoutBasketDto> GetBasket(Guid userId);
    
    public Task<bool> CreateOrder(CreateOrderRequest orderRequest);

    public Task<List<OrderDto>> GetOrders(Guid userId);
}