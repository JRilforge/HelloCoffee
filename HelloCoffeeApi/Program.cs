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
    var dbContext = scope.ServiceProvider.GetRequiredService<ShopContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();