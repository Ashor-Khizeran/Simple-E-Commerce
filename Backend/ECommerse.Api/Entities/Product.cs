namespace ECommerse.Api.Entities;

public class Product
{
    public Guid Id {get; private set;}
    public string Name {get; private set;} = default!;

    private Product(){}
}