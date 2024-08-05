using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelloCoffeeApi.Areas.Shop;

var builder = WebApplication.CreateBuilder(args);

// TODO Hide in environment variables
var cosmosEndpoint = "https://cosmosrgeastuse7675b48-6e97-481f-b598db.documents.azure.com:443/";
var cosmosKey = "b6rAUQeEjvIpczsdBmKkMMlqoxG5miQEMBniyvjr4XCV8fwkpsNOg6sU1zRFtEMSxfFjzkX4mblfACDbBi9mDQ==";
var cosmosDatabase = "HelloCoffee";

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

    PersistCoffeeShopItemsIfMissing(shopContext);
    
    var basketContext = scope.ServiceProvider.GetRequiredService<BasketContext>();
    await basketContext.Database.EnsureCreatedAsync();
    
    var orderContext = scope.ServiceProvider.GetRequiredService<OrderContext>();
    await orderContext.Database.EnsureCreatedAsync();
}

app.Run();

async void PersistCoffeeShopItemsIfMissing(ShopContext shopContext)
{
    
    
    int itemCount = await shopContext.Items.CountAsync();

    /*if ()
    {
        
    }*/
}