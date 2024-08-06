using HelloCoffee.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Newtonsoft.Json;
using SoloX.CodeQuality.Test.Helpers.Http;

namespace HelloCoffeeTestSuite.Areas.Shop.App.Service;

[TestFixture]
public class WebAppClientShopServiceTest
{
    private IShopService _shopService;

    [SetUp]
    public void SetUp()
    {
        // Do Nothing for now
    }

    [Test]
    public async Task GetShopItemsFor_Retrieves_AllCoffeeItems()
    {
        // Arrange
        var expectedCoffeeItems = new List<ShopItemDto>(Array
            .ConvertAll(ShopItemConstants.Coffee, item => new ShopItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price
            }));
        
        var httpClient = new HttpClientMockBuilder()
            .WithBaseAddress(new Uri("http://host"))
            .WithRequest($"/shop/category/0")
            .RespondingJsonContent<List<ShopItemDto>>(
                request => expectedCoffeeItems)
            .Build();
        
        _shopService = new ClientShopService(httpClient);
        
        // Act
        var coffeeItemList = await _shopService.GetShopItemsFor((int) ItemSubCategory.Coffee);
        
        Assert.NotNull(coffeeItemList);
        Assert.That(coffeeItemList.Count, Is.EqualTo(ShopItemConstants.Coffee.Length));
        Assert.That(ToJson(coffeeItemList), Is.EqualTo(ToJson(expectedCoffeeItems)));
    }

    private static string ToJson(List<ShopItemDto> source)
    {
        return JsonConvert.SerializeObject(source);
    }
}