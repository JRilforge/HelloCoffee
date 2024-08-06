using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Microsoft.Playwright;

namespace HelloCoffeeTestSuite.Areas.Shop.App.Controller;

// https://playwright.dev/dotnet/docs/api-testing

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class HelloCoffeeControllerIntegrationTests : PlaywrightTest
{
    private IAPIRequestContext request = null!;
    private IAPIRequestContext apiRequest = null;

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
            BaseURL = TestUtils.HelloCoffeeAppEndpoint,
            ExtraHTTPHeaders = headers,
        });
        
        apiRequest = await Playwright.APIRequest.NewContextAsync(new() {
            // All requests we send go to this API endpoint.
            BaseURL = TestUtils.HelloCoffeeApiEndpoint,
            ExtraHTTPHeaders = headers,
        });
    }
    
    [Test]
    public async Task CreateOrder_Creates_TheRequestedOver()
    {
        var userId = Guid.NewGuid();
        
        await apiRequest.PostAsync("/basket/items", new() {
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
        
        Assert.That(response.Url, Is.EqualTo("http://localhost:5128/OrderComplete"));
    }

    [TearDown]
    public async Task TearDownApiTesting()
    {
        await request.DisposeAsync();
        await apiRequest.DisposeAsync();
    }
}