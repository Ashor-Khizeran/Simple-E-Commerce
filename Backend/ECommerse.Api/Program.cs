using System.Text.Json;
using ECommerse.Api.Data;
using Microsoft.EntityFrameworkCore;
using ECommerse.Api.Entities;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);

// Get Microsoft SQL server connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ECommerseDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();


app.MapPost("api/products", async (ECommerseDbContext context, Product searchedProduct) =>
{
    try
    {
        // Search for the product in database
        var existingProduct = await context.products.FirstOrDefaultAsync<Product>(p => p.Name == searchedProduct.Name);
        // Found the product
        if(existingProduct is not null)
        {
            return Results.Ok(existingProduct);

        }
    }
    catch (Exception ex)
    {
        // Log the errors
        return Results.BadRequest(new{messeage = ex.Message});
    }

    return Results.NotFound("Not found!");
});

app.Run();
