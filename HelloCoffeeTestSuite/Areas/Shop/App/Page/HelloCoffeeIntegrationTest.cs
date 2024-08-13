using HelloCoffeeApiClient.Areas.Shop.Data.Type;
using Microsoft.Playwright;

namespace HelloCoffeeTestSuite.Areas.Shop.App.Page;

// https://terencegolla.com/.net/unit-testing-asp-net-core-identity/

[TestFixture]
public class HelloCoffeeIntegrationTests : PageTest
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
                
            var itemName = Page.Locator($".col:has([data-shop-item-id]):nth-child({i + 1}) .shop-item-name");
            var itemPrice = Page.Locator($".col:has([data-shop-item-id]):nth-child({i + 1}) .shop-item-price");
                
            await Expect(itemName).ToHaveTextAsync(item.Name);
            await Expect(itemPrice).ToHaveTextAsync($"{item.Price:c}");
        }
    }
    
    [Test]
    public async Task WhenAddToBasketClicked_PageShould_IndicateThatItemHasBeenAddedToBasket()
    {
        await LoginWithPlaywrightTestUser();
        
        // initially no items in the basket
        
        var checkoutBasketLinkItemCount = Page.Locator("#checkout-basket-link .badge");

        await Expect(checkoutBasketLinkItemCount).ToHaveTextAsync("0");
        
        // add one item to the basket
        
        var addToBasketBtn = Page.Locator(".add-shop-item-to-basket-btn").First;

        await addToBasketBtn.ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        
        // has 1 item in the basket
        
        checkoutBasketLinkItemCount = Page.Locator("#checkout-basket-link .badge");
        
        await Expect(checkoutBasketLinkItemCount).ToHaveTextAsync("1");

        var shopItemUnitCountView = Page.Locator(".shop-item-unit-count").First;
        
        await Expect(shopItemUnitCountView).ToHaveValueAsync("1");
    }
    
    [Test]
    public async Task WhenAddToBasketClicked_ThenRemoveFromBasketClicked_PageShould_IndicateThatNoItemsInBasket()
    {
        // Added an item to the basket and asserts
        await WhenAddToBasketClicked_PageShould_IndicateThatItemHasBeenAddedToBasket();
        
        // remove item from the basket
        
        var removeFromBasketBtn = Page.Locator(".remove-shop-item-from-basket-btn").First;

        await removeFromBasketBtn.ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        
        // has 0 items in the basket
        
        var checkoutBasketLinkItemCount = Page.Locator("#checkout-basket-link .badge");
        
        await Expect(checkoutBasketLinkItemCount).ToHaveTextAsync("0");

        var shopItemUnitCountView = Page.Locator(".shop-item-unit-count").First;
        
        await Expect(shopItemUnitCountView).ToHaveValueAsync("0");
    }

    [Test]
    public async Task WhenAnItemIsInTheBasket_CompleteOrder_SeeOrderCompletePage()
    {
        // Added an item to the basket and asserts
        await WhenAddToBasketClicked_PageShould_IndicateThatItemHasBeenAddedToBasket();

        // navigate to checkout page

        var checkoutBasketLink = Page.Locator("#checkout-basket-link");

        await checkoutBasketLink.ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        // Complete order (address and payment details are auto populated)

        var checkoutBasketTotalCount = Page.Locator("#basket-items-total-count");

        await Expect(checkoutBasketTotalCount).ToHaveTextAsync("1");

        var basketItemUnitCount = Page.Locator(".basket-item-unit-count").First;

        await Expect(basketItemUnitCount).ToHaveTextAsync("Quantity: 1");

        var basketItemTotalCost = Page.Locator(".basket-item-total-cost").First;

        await Expect(basketItemTotalCost).ToHaveTextAsync($"{ShopItemConstants.Coffee[0].Price:c}");

        var completeOrderBtn = Page.Locator("text=Complete Order");
        
        await completeOrderBtn.ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        
        // check we are on the OrderComplete page

        await Expect(Page).ToHaveURLAsync($"{TestUtils.HelloCoffeeAppEndpoint}/OrderComplete");
        
        var h1Heading = Page.Locator("h1").First;

        await Expect(h1Heading).ToHaveTextAsync("Order Completed");
        
        var pElement = Page.Locator("p").First;

        await Expect(pElement).ToHaveTextAsync("Your order is on its way. Have a nice day!");
    }

    private async Task LoginWithPlaywrightTestUser()
    {
        await Page.GotoAsync($"{TestUtils.HelloCoffeeAppEndpoint}/Identity/Account/Login");
        
        // If using JetBrains Rider set environment variable in Settings > Unit Testing > Test Runner
        
        string password = Environment.GetEnvironmentVariable("PLAYWRIGHT_USER_PASSWORD") ?? 
                          throw new Exception("Please populated the 'PLAYWRIGHT_USER_PASSWORD' environment variable");
        
        // Interact with login form
        await Page.Locator("#Input_Email").FillAsync("playwright.user@test.co.uk");
        await Page.Locator("#Input_Password").FillAsync(password);
        await Page.Locator("#login-submit").ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

        await Expect(Page).ToHaveURLAsync(TestUtils.HelloCoffeeAppEndpoint + "/");
        
        // Clean up the basket

        await CleanUpBasket();
    }

    private async Task CleanUpBasket()
    {
        var checkoutBasketLinkItemCount = Page.Locator("#checkout-basket-link .badge");

        var basketItemCount = await checkoutBasketLinkItemCount.TextContentAsync();

        if (basketItemCount != null && !basketItemCount.Equals("0"))
        {
            // get userId from the current page

            var userIdHiddenInput = Page.Locator("[name=\"UserId\"]").First;

            var userId = await userIdHiddenInput.InputValueAsync();
            
            // clear the basket

            var headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/json");

            var apiRequest = await Playwright.APIRequest.NewContextAsync(new()
            {
                // All requests we send go to this API endpoint.
                BaseURL = TestUtils.HelloCoffeeApiEndpoint,
                ExtraHTTPHeaders = headers,
            });
        
            var response = await apiRequest.DeleteAsync($"/basket/items?userId={userId}");
            await Expect(response).ToBeOKAsync();

            await apiRequest.DisposeAsync();

            // To reflect change on the api side
            
            await Page.ReloadAsync();
            
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            
            await Expect(Page).ToHaveURLAsync(TestUtils.HelloCoffeeAppEndpoint + "/");
        }
    }
}