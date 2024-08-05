using System.Net;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HelloCoffee.Areas.Shop;

public class ClientCheckoutService(HttpClient client) : ICheckoutService
{
    public async Task<bool> AddItemToBasket(AddItemToBasketRequest request)
    {
        var response = await client.PostAsJsonAsync($"basket/items", request);
        
        return (int) response.StatusCode == 200;
    }

    public async Task<CheckoutBasketDto> GetBasket(Guid userId)
    {
        return await client.GetFromJsonAsync<CheckoutBasketDto>($"basket?userId={userId}") ?? new ();
    }

    public async Task<bool> CreateOrder(CreateOrderRequest orderRequest)
    {
        var response = await client.PostAsJsonAsync("orders", orderRequest);
        
        return (int) response.StatusCode == 200;
    }

    public async Task<List<OrderDto>> GetOrders(Guid userId)
    {
        return await client.GetFromJsonAsync<List<OrderDto>>("orders") ?? [];
    }
}