namespace Ecommerce;

public class Cart
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public List<CartItem> Items { get; set; }
    public decimal Total => CalculateTotal();
    public int Version { get; set; }
    public List<Coupon> Coupons { get; set; }
    
    private decimal CalculateTotal()
    {
        return Items.Sum(ApplyCouponToItem);
    }

    private decimal ApplyCouponToItem(CartItem item)
    {
        var validCoupons = Coupons.Where(c => c.ExpiresAt >= DateTimeOffset.Now && c.ValidFor.Contains(item.Category)).ToList();
        return item.Price * (1 - validCoupons.Sum(c => c.Discount) / 100);
    }
}
public record CartItem(Guid ProductId, string Name, decimal Price, uint Quantity, ProductCategory Category);
public record SetCartItem(Guid ProductId, uint Quantity);
