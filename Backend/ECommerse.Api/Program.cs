using System.Text.Json;
using ECommerse.Api.Data;
using Microsoft.EntityFrameworkCore;
using ECommerse.Api.Entities;

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


app.MapPost("api/products", async (HttpRequest searchRequest) =>
{
    var searchProduct = await JsonSerializer.DeserializeAsync<Product>(searchRequest.Body);

    return Results.Ok(searchProduct);
});

app.Run();
