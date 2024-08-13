using HelloCoffee.Areas.Identity.Data;
using HelloCoffee.Areas.Shop;
using Microsoft.EntityFrameworkCore;
using HelloCoffee.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(sp => 
    new HttpClient
    {
        BaseAddress = new Uri($"https://{Environment.GetEnvironmentVariable("HELLO_COFFEE_API_HOST")}")
    });

builder.Services.AddScoped<IShopService, ClientShopService>();
builder.Services.AddScoped<ICheckoutService, ClientCheckoutService>();

// review https://medium.com/@kevinwilliams.dev/ef-core-cosmos-db-3da250b47d6c

var cosmosEndpoint = Environment.GetEnvironmentVariable("COSMOS_ENDPOINT") ?? 
                     throw new Exception("Please populated the 'COSMOS_ENDPOINT' environment variable");
var cosmosKey = Environment.GetEnvironmentVariable("COSMOS_KEY") ?? 
                throw new Exception("Please populated the 'COSMOS_KEY' environment variable");
var cosmosDatabase = Environment.GetEnvironmentVariable("COSMOS_DB") ?? 
                     throw new Exception("Please populated the 'COSMOS_DB' environment variable");

// Identity

builder.Services.AddDbContext<HelloCoffeeContext>(options =>
    options.UseCosmos(cosmosEndpoint, cosmosKey, cosmosDatabase));

/*
 * builder.Services.AddDbContext<CustomerDbContext>(options =>
   options.UseInMemoryDatabase("name"));
 */

builder.Services.AddDefaultIdentity<HelloCoffeeUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HelloCoffeeContext>();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();

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

app.MapControllers();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HelloCoffeeContext>();
    await dbContext.Database.EnsureCreatedAsync();

    await CreatePlaywrightTestUser(scope);
}

app.Run();

async Task CreatePlaywrightTestUser(AsyncServiceScope scope)
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<HelloCoffeeUser>>();
    
    string userName = "playwright.user@test.co.uk";
    string password = Environment.GetEnvironmentVariable("PLAYWRIGHT_USER_PASSWORD") ?? 
                      throw new Exception("Please populated the 'PLAYWRIGHT_USER_PASSWORD' environment variable");

    var existingUser = await userManager.FindByEmailAsync(userName);

    if (existingUser == null)
    {
        HelloCoffeeUser user = new HelloCoffeeUser()
        {
            UserName = userName,
            Email = password,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, password);
    }
}