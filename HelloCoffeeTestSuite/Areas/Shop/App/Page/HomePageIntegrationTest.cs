using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Microsoft.Playwright;

namespace HelloCoffeeTestSuite.Areas.Shop.App.Page;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class HomePageIntegrationUnitTests : PageTest
{
    [Test]
    public async Task NavigationToRootPath_Shows_HomePage()
    {
        await Page.GotoAsync(TestUtils.HelloCoffeeAppEndpoint);

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Home - HelloCoffee"));

        // create a locator
        var selectedPageNavLink = Page.Locator(".nav-link.active");

        await Expect(selectedPageNavLink).ToBeVisibleAsync();
        await Expect(selectedPageNavLink).ToHaveAttributeAsync("href", "/shop/category/0");

        var subCategoryHeading = Page.Locator("#sub-category-heading");
        await Expect(subCategoryHeading).ToHaveTextAsync("Coffee");
    }
    
    [Test]
    public async Task ClickSandwichNavLink_Should_NavigateToSandwichItemsPage()
    {
        await Page.GotoAsync(TestUtils.HelloCoffeeAppEndpoint);
        
        var sandwichPageNavLink = Page.Locator("text=Sandwich");

        await sandwichPageNavLink.ClickAsync();

        await Page.WaitForURLAsync(new Regex(".*/shop/category/5$"));
        
        for (var i = 0; i < ShopItemConstants.Sandwich.Length; i++)
        {
            var item = ShopItemConstants.Sandwich[i];
                
            var itemName = Page.Locator($"[data-shop-item-id]:nth-child({i + 1}) .shop-item-name");
            var itemPrice = Page.Locator($"[data-shop-item-id]:nth-child({i + 1}) .shop-item-price");
                
            await Expect(itemName).ToHaveTextAsync(item.Name);
            await Expect(itemPrice).ToHaveTextAsync($"{item.Price:n}");
        }
    }
    
    [Test]
    public async Task WhenAddToBasketClicked_PageShould_IndicateThatItemHasBeenAddedToBasket()
    {
        await Page.GotoAsync(TestUtils.HelloCoffeeAppEndpoint);

        var checkoutBasketLinkItemCount = Page.Locator(".checkout-basket-link .badge");

        await Expect(checkoutBasketLinkItemCount).ToHaveTextAsync("0");
        
        var addToBasketBtn = Page.Locator("text=Add To Basket").First;

        await addToBasketBtn.ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        var checkoutBasketLink = Page.Locator(".checkout-basket-link");

        await checkoutBasketLink.ClickAsync();
        
        checkoutBasketLinkItemCount = Page.Locator(".checkout-basket-link .badge");
        
        await Expect(checkoutBasketLinkItemCount).ToHaveTextAsync("1");
    }
}