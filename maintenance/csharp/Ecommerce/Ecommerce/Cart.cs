namespace Ecommerce;

public class Cart
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<CartItem> Items { get; set; }
    public List<Coupon> Coupons { get; set; }
    public decimal Total { get => Items.Sum(i => i.Price);}
    public int Version { get; set; }
}

public record CartItem(Guid ProductId, string Name, decimal Price, uint Quantity);

public record Coupon(Guid Id, Guid CartId, string Code, decimal Discount);

public record AddCartItem(Guid ProductId, uint Quantity);

public record ProductRef(Guid Id, string Name, decimal Price);
