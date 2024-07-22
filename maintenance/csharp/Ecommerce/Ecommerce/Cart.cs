namespace Ecommerce;

public class Cart
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<CartItem> Items { get; set; }
    public List<Coupon> Coupons { get; set; }
    public decimal Total { get; set; }
}

public record CartItem(Guid Id, Guid CartId, Guid ProductId, uint Quantity);

public record Coupon(Guid Id, Guid CartId, string Code, decimal Discount);
