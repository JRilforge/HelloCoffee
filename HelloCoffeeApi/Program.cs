using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelloCoffeeApi.Areas.Shop;
using HelloCoffeeApiClient.Areas.Shop.Data;
using HelloCoffeeApiClient.Areas.Shop.Data.Type;

var builder = WebApplication.CreateBuilder(args);

var cosmosEndpoint = Environment.GetEnvironmentVariable("COSMOS_ENDPOINT") ?? 
                     throw new Exception("Please populated the 'COSMOS_ENDPOINT' environment variable");
var cosmosKey = Environment.GetEnvironmentVariable("COSMOS_KEY") ?? 
                throw new Exception("Please populated the 'COSMOS_KEY' environment variable");
var cosmosDatabase = Environment.GetEnvironmentVariable("COSMOS_DB") ?? 
                     throw new Exception("Please populated the 'COSMOS_DB' environment variable");

// Inventory
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseCosmos(cosmosEndpoint, cosmosKey, cosmosDatabase));

// Basket
builder.Services.AddDbContext<BasketContext>(options =>
    options.UseCosmos(cosmosEndpoint, cosmosKey, cosmosDatabase));

// Basket
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseCosmos(cosmosEndpoint, cosmosKey, cosmosDatabase));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

using (var scope = app.Services.CreateAsyncScope())
{
    var shopContext = scope.ServiceProvider.GetRequiredService<ShopContext>();
    await shopContext.Database.EnsureCreatedAsync();

    await PersistCoffeeShopItemsIfMissing(shopContext);
    
    var basketContext = scope.ServiceProvider.GetRequiredService<BasketContext>();
    await basketContext.Database.EnsureCreatedAsync();
    
    var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
    await orderContext.Database.EnsureCreatedAsync();
}

app.Run();

async Task PersistCoffeeShopItemsIfMissing(ShopContext shopContext)
{
    int itemCount = await shopContext.Items.CountAsync();

    if (itemCount != ShopItemConstants.ItemCount)
    {
        List<ShopItem> items = new();
        
        foreach (var subCategoryItemsEntry in ShopItemConstants.SubCategoryItemsMap)
        {
            var subCategory = subCategoryItemsEntry.Key;
            var category = ShopItemConstants.GetCategory(subCategory);
                
            foreach (var item in subCategoryItemsEntry.Value)
            {
                item.Category = (int) category;
                item.SubCategory = (int) subCategory;
                
                items.Add(item);
            }    
        }

        await shopContext.Items.AddRangeAsync(items);
        await shopContext.SaveChangesAsync();
    }
}