using HelloCoffee.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using SoloX.CodeQuality.Test.Helpers.Http;

namespace HelloCoffeeTestSuite.Areas.Shop.App.Service;

// https://medium.com/younited-tech-blog/easy-httpclient-mocking-3395d0e5c4fa

[TestFixture]
public class WebAppClientCheckoutServiceTest
{
    [SetUp]
    public void SetUp()
    {
        // Do Nothing for now
    }

    [Test]
    public async Task AddItemToBasket_Adds_TheRequestedItemToTheBasket()
    {
        // Arrange
        var userId = Guid.NewGuid();
        
        var httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(new Uri("http://host"))
            .WithJsonContentRequest<AddItemToBasketRequest>("/basket/items", HttpMethod.Post)
            .RespondingJsonContent(request => true)
            .Build();
        
        var checkoutService = new ClientCheckoutService(httpClient);
        
        // Act
        var added = await checkoutService.AddItemToBasket(new AddItemToBasketRequest()
        {
            ItemId = ShopItemConstants.Coffee[0].Id,
            UserId = userId,
            UnitCountModification = 1
        });
        
        // Assert
        Assert.That(added, Is.True);
    }

    [Test]
    public async Task CreateOrder_Creates_TheRequestedOver()
    {
        // Arrange
        var httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(new Uri("http://host"))
            .WithJsonContentRequest<CreateOrderRequest>("/orders", HttpMethod.Post)
            .RespondingJsonContent(request => true)
            .Build();
        
        var checkoutService = new ClientCheckoutService(httpClient);
        
        // Act
        var created = await checkoutService.CreateOrder(new CreateOrderRequest()
        {
            UserId = Guid.NewGuid(),
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
        });
        
        // Assert
        Assert.That(created, Is.True);
    }
    
    [Test]
    public async Task GetBasket_Gets_TheBasketForTheCurrentUser()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var expectedItemInBasket = ShopItemConstants.Coffee[0];
        
        var httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(new Uri("http://host"))
            .WithRequest($"/basket")
            .RespondingJsonContent(request => new CheckoutBasketDto()
            {
                UserId = userId,
                Id = Guid.NewGuid(),
                Items = new Dictionary<Guid, BasketItem>()
                {
                    {
                        expectedItemInBasket.Id, new ()
                        {
                            ItemId = expectedItemInBasket.Id,
                            UnitCost = expectedItemInBasket.Price,
                            UnitCount = 1
                        }
                    }
                }
            })
            .Build();
        
        var checkoutService = new ClientCheckoutService(httpClient);
        
        // Act
        var basket = await checkoutService.GetBasket(userId);
        
        // Assert
        Assert.NotNull(basket);
        Assert.That(basket.UserId, Is.EqualTo(userId));
        Assert.That(basket.Items.Count, Is.EqualTo(1));
        
        var actualItemInBasket = basket.Items[expectedItemInBasket.Id];
        
        Assert.NotNull(actualItemInBasket);
        Assert.That(actualItemInBasket.ItemId, Is.EqualTo(expectedItemInBasket.Id));
        Assert.That(actualItemInBasket.UnitCost, Is.EqualTo(expectedItemInBasket.Price));
        Assert.That(actualItemInBasket.UnitCount, Is.EqualTo(1));
    }
    
    [Test]
    public async Task GetOrders_Gets_AllOrdersForThisUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var orderId = Guid.NewGuid();

        var expectedItemInOrdr = ShopItemConstants.Coffee[0];
        
        var httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(new Uri("http://host"))
            .WithRequest("/orders")
            .RespondingJsonContent(request => new List<OrderDto>()
            {
                new()
                {
                    Id = orderId,
                    Paid = true,
                    BasketItems = new ()
                    {
                        {
                            expectedItemInOrdr.Id, new ()
                            {
                                ItemId = expectedItemInOrdr.Id,
                                UnitCost = expectedItemInOrdr.Price,
                                UnitCount = 1
                            }
                        }
                    }
                }
            })
            .Build();
        
        var checkoutService = new ClientCheckoutService(httpClient);
        
        // Act
        var orders = await checkoutService.GetOrders(userId);
        
        // Assert
        Assert.NotNull(orders);
        Assert.That(orders.Count, Is.EqualTo(1));
        
        var actualOrder = orders[0];
        
        Assert.NotNull(actualOrder);
        Assert.That(actualOrder.Id, Is.EqualTo(orderId));
        Assert.That(actualOrder.Paid, Is.True);
        
        var actualItemInOrder = actualOrder.BasketItems[expectedItemInOrdr.Id];
        
        Assert.NotNull(actualItemInOrder);
        Assert.That(actualItemInOrder.ItemId, Is.EqualTo(expectedItemInOrdr.Id));
        Assert.That(actualItemInOrder.UnitCost, Is.EqualTo(expectedItemInOrdr.Price));
        Assert.That(actualItemInOrder.UnitCount, Is.EqualTo(1));
    }
}