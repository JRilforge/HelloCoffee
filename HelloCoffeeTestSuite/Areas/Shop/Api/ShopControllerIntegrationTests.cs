using System.Text.Json;
using HelloCoffeeApiClient.Areas.Shop.Data.Dto;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Microsoft.Playwright;
using Newtonsoft.Json;

namespace HelloCoffeeTestSuite.Areas.Shop.Api;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ShopControllerIntegrationTests : PlaywrightTest
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
    public async Task GetShopItemsFor_Retrieves_AllCoffeeItems()
    {
        
        var subCategoryItemsResponse = await request.GetAsync($"shop/category/{(int) ItemSubCategory.Coffee}");
        await Expect(subCategoryItemsResponse).ToBeOKAsync();

        var subCategoryItemsString = await subCategoryItemsResponse.TextAsync();

        var subCategoryItems = JsonConvert.DeserializeObject<List<ShopItemDto>>(subCategoryItemsString);
        
        Assert.NotNull(subCategoryItems);
        Assert.That(subCategoryItems.Count, Is.EqualTo(ShopItemConstants.Coffee.Length));
        
        var itemIds = subCategoryItems.Select(item => item.Id);
        Assert.That(ShopItemConstants.Coffee.Select(item => item.Id), Is.EqualTo(itemIds));
    }

    [TearDown]
    public async Task TearDownApiTesting()
    {
        await request.DisposeAsync();
    }
    
    // for later - NavigationToRootPath_Shows_HomePage
}