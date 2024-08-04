using HelloCoffee.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HelloCoffee.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HelloCoffeeContextConnection") ?? throw new InvalidOperationException("Connection string 'HelloCoffeeContextConnection' not found.");

builder.Services.AddDbContext<HelloCoffeeContext>(options => options.UseSqlServer(connectionString));

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

app.Run();