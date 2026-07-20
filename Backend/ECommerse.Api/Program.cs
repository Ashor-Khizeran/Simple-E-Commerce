using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

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

record Product(Guid Id, string Name);
