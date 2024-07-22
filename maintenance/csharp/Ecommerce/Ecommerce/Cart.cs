namespace Ecommerce;

public class Cart
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<CartItem> Items { get; set; }
    public List<Coupon> Coupons { get; set; }
}

public class CartItem
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public uint Quantity { get; set; }
}

public class Coupon
{
    public Guid Id { get; set; }
    public Guid CartId { get; set; }
    public string Code { get; set; }
    public decimal Discount { get; set; }
}
