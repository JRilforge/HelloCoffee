using HelloCoffee.Areas.Shop;
using HelloCoffee.Areas.Shop.Controller;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HelloCoffeeTestSuite.Areas.Shop.App.Controller;

[TestFixture]
public class HelloCoffeeControllerTest
{
    [Test]
    public async Task AddItemToBasket_Adds_TheRequestedItemToTheBasket()
    {
        // Arrange
        var createOrderRequest = new CreateOrderRequest()
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
        };

        var mockCheckoutService = new Mock<ICheckoutService>();
        
        var helloCoffeeController = new HelloCoffeeController(mockCheckoutService.Object);

        mockCheckoutService.Setup(service => service.CreateOrder(createOrderRequest))
            .Returns(Task.FromResult(true));
        
        // Act
        var result = await helloCoffeeController.CreateOrder(createOrderRequest);
        
        // Assert
        Assert.NotNull(result);
        Assert.That(result, Is.InstanceOf<RedirectResult>());
        Assert.That((result as RedirectResult)?.Url, Is.EqualTo("OrderComplete"));

    }
}