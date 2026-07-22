namespace ECommerse.Api.Data;

using Microsoft.EntityFrameworkCore;
using ECommerse.Api.Entities;

internal class ECommerseDbContext: DbContext
{
    // Required constructor for AddDbContext DI configuration
    public ECommerseDbContext(DbContextOptions<ECommerseDbContext> options): base(options)
    {
    }
    public DbSet<Product> products {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityTypeConfiguration<Product>).Assembly);
    }
}