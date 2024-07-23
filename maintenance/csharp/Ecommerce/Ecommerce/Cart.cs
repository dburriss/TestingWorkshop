namespace Ecommerce;

public class Cart
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<CartItem> Items { get; set; }
    public decimal Total { get => Items.Sum(i => i.Price);}
    public int Version { get; set; }
}
public record CartItem(Guid ProductId, string Name, decimal Price, uint Quantity);
public record SetCartItem(Guid ProductId, uint Quantity);
