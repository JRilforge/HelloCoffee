using HelloCoffee.Areas.Identity.Data;
using HelloCoffee.Areas.Shop;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelloCoffee.Data;
var builder = WebApplication.CreateBuilder(args);

// review https://medium.com/@kevinwilliams.dev/ef-core-cosmos-db-3da250b47d6c

// Hide in environment variables
var cosmosEndpoint = "https://cosmosrgeastuse7675b48-6e97-481f-b598db.documents.azure.com:443/";
var cosmosKey = "b6rAUQeEjvIpczsdBmKkMMlqoxG5miQEMBniyvjr4XCV8fwkpsNOg6sU1zRFtEMSxfFjzkX4mblfACDbBi9mDQ==";
var cosmosDatabase = "HelloCoffee";
// mock user password: j#ZiHLwb.4Lt_#7

// Identity
builder.Services.AddDbContext<HelloCoffeeContext>(options =>
    options.UseCosmos(cosmosEndpoint, cosmosKey, cosmosDatabase));

// Inventory
builder.Services.AddDbContext<ShopContext>(options =>
    options.UseCosmos(cosmosEndpoint, cosmosKey, cosmosDatabase));



/*
 * builder.Services.AddDbContext<CustomerDbContext>(options =>
   options.UseInMemoryDatabase("name"));
 */

builder.Services.AddDefaultIdentity<HelloCoffeeUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HelloCoffeeContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HelloCoffeeContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();
