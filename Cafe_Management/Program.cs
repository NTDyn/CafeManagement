


using Cafe_Management.Application.Services;
using Cafe_Management.Core.Interfaces;
using Cafe_Management.Infrastructure.Data;
using Cafe_Management.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
        builder => builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
//Product
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
// Warehouse
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<WarehouseService>();
// Product Category
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<ProductCategoryService>();
// Supplier
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<SupplierService>();
// Ingredient
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IngredientService>();
// Ingredient Category
builder.Services.AddScoped<IIngredientCategoryRepository, IngredientCategoryRepository>();
builder.Services.AddScoped<IngredientCategoryService>();
// Product Recipe
builder.Services.AddScoped<IProductRecipeRepository, ProductRecipeRepository>();
builder.Services.AddScoped<ProductRecipeService>();
// Menu
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<MenuService>();
// Menu
builder.Services.AddScoped<IMenuDetailRepository, MenuDetailRepository>();
builder.Services.AddScoped<MenuDetailRepository>();
//Save Image 
builder.Services.AddScoped<IImageRepository, SaveImageRepository>();
builder.Services.AddScoped<ImageService>();

//Staff
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<StaffRepository>();

//Staff
builder.Services.AddScoped<IStaffGroupPermissionResponsitory, StaffGroupLinkPermissionRepository>();
builder.Services.AddScoped<StaffGroupLinkPermissionRepository>();

//Staff
builder.Services.AddScoped<IStaffGroupRepository, StaffGroupRepository>();
builder.Services.AddScoped<StaffGroupRepository>();

var app = builder.Build();
app.UseCors("CorsApi");
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowAll");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
