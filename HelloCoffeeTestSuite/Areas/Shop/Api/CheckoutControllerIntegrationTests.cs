using System.Text.Json;
using HelloCoffee.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Microsoft.Playwright;
using Newtonsoft.Json;

namespace HelloCoffeeTestSuite.Areas.Shop.Api;

// https://playwright.dev/dotnet/docs/api-testing

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class CheckoutControllerIntegrationTests : PlaywrightTest
{
    private IAPIRequestContext request = null!;

    [SetUp]
    public async Task SetUpApiTesting()
    {
        await CreateApiRequestContext();
    }

    private async Task CreateApiRequestContext()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        request = await Playwright.APIRequest.NewContextAsync(new() {
            // All requests we send go to this API endpoint.
            BaseURL = "http://localhost:5238",
            ExtraHTTPHeaders = headers,
        });
    }
    
    [Test]
    public async Task AddItemToBasket_Adds_TheRequestedItemToTheBasket()
    {
        var userId = Guid.NewGuid();

        var expectedItemInBasket = ShopItemConstants.Coffee[0];
        
        var response = await request.PostAsync("/basket/items", new() {
            DataObject = new AddItemToBasketRequest()
            {
                ItemId = ShopItemConstants.Coffee[0].Id,
                UserId = userId,
                UnitCountModification = 1
            }});
        await Expect(response).ToBeOKAsync();

        var result = await response.TextAsync();

        bool.TryParse(result, out var itemAddedToBasket);
        
        Assert.That(itemAddedToBasket, Is.True);
        
        var basketResponse = await request.GetAsync($"/basket?userId={userId}");
        await Expect(basketResponse).ToBeOKAsync();
        
        var basketResult = await basketResponse.TextAsync();

        var basket = basketResult.ToType<CheckoutBasketDto>();
        
        Assert.NotNull(basket);
        Assert.That(basket.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(basket.Items.Count, Is.EqualTo(1));

        var basketItem = basket.Items[expectedItemInBasket.Id];
        
        Assert.NotNull(basketItem);
        Assert.That(basketItem.ItemId, Is.EqualTo(expectedItemInBasket.Id));
        Assert.That(basketItem.UnitCost, Is.EqualTo(expectedItemInBasket.Price));
        Assert.That(basketItem.UnitCount, Is.EqualTo(1));
    }
    
    [Test]
    public async Task CreateOrder_Creates_TheRequestedOver()
    {
        var userId = Guid.NewGuid();
        
        await request.PostAsync("/basket/items", new() {
            DataObject = new AddItemToBasketRequest()
            {
                ItemId = ShopItemConstants.Coffee[0].Id,
                UserId = userId,
                UnitCountModification = 1
            }});

        var requestPayload = new CreateOrderRequest()
        {
            UserId = userId,
            FirstName = "John",
            LastName = "Doe",

            Address = new()
            {
                FirstLine = "1 Imaginary Street",
                SecondLine = "Fake Avenue",
                City = "London",
                County = "Greater London",
                Country = "United Kingdom",
                PostCode = "ST6 2AH"
            },

            CardNumber = "7894-3244-3242-5489",
            NameOnCard = "Mr John Doe",
            Expiration = "12/2040",
            Cvv = "368"
        };
        
        var response = await request.PostAsync("/orders", new() {
            DataObject = requestPayload});
        await Expect(response).ToBeOKAsync();

        var result = await response.TextAsync();

        bool.TryParse(result, out var itemAddedToBasket);
        
        Assert.That(itemAddedToBasket, Is.True);

        var ordersResponse = await request.GetAsync($"/orders?userId={userId}");
        await Expect(ordersResponse).ToBeOKAsync();
        
        var ordersResult = await ordersResponse.TextAsync();

        var orders = ordersResult.ToType<List<OrderDto>>();
        
        Assert.NotNull(orders);
        Assert.That(orders.Count, Is.EqualTo(1));

        var order = orders[0];
        
        Assert.NotNull(order);
        Assert.That(order.Id, Is.Not.EqualTo(Guid.Empty));
        Assert.That(order.Paid, Is.True);
    }

    [Test]
    public async Task PopulatingTheBasket_ThenOrdering_ShouldClearTheBasket()
    {
        var userId = Guid.NewGuid();
        
        await request.PostAsync("/basket/items", new() {
            DataObject = new AddItemToBasketRequest()
            {
                ItemId = ShopItemConstants.Juice[0].Id,
                UserId = userId,
                UnitCountModification = 1
            }});
        await request.PostAsync("/basket/items", new() {
            DataObject = new AddItemToBasketRequest()
            {
                ItemId = ShopItemConstants.Sandwich[0].Id,
                UserId = userId,
                UnitCountModification = 2
            }});
        await request.PostAsync("/basket/items", new() {
            DataObject = new AddItemToBasketRequest()
            {
                ItemId = ShopItemConstants.Snack[0].Id,
                UserId = userId,
                UnitCountModification = 4
            }});
        
        // Check the basket for these items
        
        var basketResponse = await request.GetAsync($"/basket?userId={userId}");
        await Expect(basketResponse).ToBeOKAsync();
        
        var basketResult = await basketResponse.TextAsync();

        var basket = basketResult.ToType<CheckoutBasketDto>();
        
        Assert.NotNull(basket);
        Assert.That(basket.Items.Count, Is.EqualTo(3));

        var juiceItem = basket.Items[ShopItemConstants.Juice[0].Id];
        Assert.NotNull(juiceItem);
        Assert.That(juiceItem.UnitCount, Is.EqualTo(1));

        var sandwichItem = basket.Items[ShopItemConstants.Sandwich[0].Id];
        Assert.NotNull(sandwichItem);
        Assert.That(sandwichItem.UnitCount, Is.EqualTo(2));

        var snackItem = basket.Items[ShopItemConstants.Snack[0].Id];
        Assert.NotNull(snackItem);
        Assert.That(snackItem.UnitCount, Is.EqualTo(4));
        
        // Order the items in the basket
        
        await request.PostAsync("/orders", new() {
            DataObject = new CreateOrderRequest()
            {
                UserId = userId,
                FirstName = "John",
                LastName = "Doe",

                Address = new()
                {
                    FirstLine = "1 Imaginary Street",
                    SecondLine = "Fake Avenue",
                    City = "London",
                    County = "Greater London",
                    Country = "United Kingdom",
                    PostCode = "ST6 2AH"
                },

                CardNumber = "7894-3244-3242-5489",
                NameOnCard = "Mr John Doe",
                Expiration = "12/2040",
                Cvv = "368"
            }});
        
        // Check the basket is now empty
        
        var afterBasketResponse = await request.GetAsync($"/basket?userId={userId}");
        await Expect(afterBasketResponse).ToBeOKAsync();
        
        var afterBasketResult = await afterBasketResponse.TextAsync();

        var afterBasket = afterBasketResult.ToType<CheckoutBasketDto>();
        
        Assert.NotNull(afterBasket);
        Assert.That(afterBasket.Items.Count, Is.EqualTo(0));
    }

    [TearDown]
    public async Task TearDownApiTesting()
    {
        await request.DisposeAsync();
    }
    
    // for later - NavigationToRootPath_Shows_HomePage
}