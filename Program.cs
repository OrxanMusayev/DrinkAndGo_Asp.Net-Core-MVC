
using DrinkAndGo.Data;
using DrinkAndGo.Data.Interfaces;
using DrinkAndGo.Data.Models;
using DrinkAndGo.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cont = builder.Configuration.GetConnectionString("MyMacConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyMacConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IDrinkRepository, DrinkRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => ShoppingCart.GetCart(sp));
builder.Services.AddMvc(x =>
{
    x.EnableEndpointRouting = false;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Logging.AddConsole();

var app = builder.Build();


app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
//app.UseMvcWithDefaultRoute();
app.UseMvc(routes =>
{
    routes.MapRoute(name: "categoryFilter", template: "Drink/{action}/{category?}",
        defaults: new { Controller = "Drink", Action = "List" });
    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
});
using var scope = app.Services.CreateScope() ;
DbInitializer.Seed(scope.ServiceProvider.GetRequiredService<AppDbContext>());

app.Run();

