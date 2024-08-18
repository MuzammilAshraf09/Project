using Microsoft.Win32;
using Project.Models.Interfaces;
using Project.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IReturnRepository, ReturnRepository>();
builder.Services.AddScoped<IProductReviewRepository, ProductReviewRepository>();

builder.Services.Decorate<IProductRepository, ProductRepositoryDecorator>();
builder.Services.Decorate<IOrderRepository, OrderRepositoryDecorator>();
builder.Services.Decorate<ICategoryRepository, CategoryRepositoryDecorator>();
builder.Services.Decorate<ICartItemsRepository, CartItemsRepositoryDecorator>();
builder.Services.Decorate<IAdminRepository, AdminRepositoryDecorator>();
builder.Services.Decorate<IUserRepository, UserRepositoryDecorator>();
builder.Services.Decorate<IOrderItemRepository, OrderItemRepositoryDecorator>();
builder.Services.Decorate<IReturnRepository, ReturnRepositoryDecorator>();
builder.Services.Decorate<IProductReviewRepository, ProductReviewRepositoryDecorator>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
